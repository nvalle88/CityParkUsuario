﻿using AppParqueoAzul.Classes;
using AppParqueoAzul.Models;
using AppParqueoAzul.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Services
{
  public class ApiService
  {
        public async Task<Response> Login(string usuario, string contrasena)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Usuario = usuario,
                    Contrasena = contrasena,
                };

                var request = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/Usuarios/Login";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o Contraseña incorrecto",
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Usuario>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Login Ok",
                    Result = user,
                };



            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }

        public async Task<Response> ConsultarSaldo(int usuarioId)
        {
            try
            {
                var consultaSaldo = new ConsultarSaldo
                {
                    UsuarioId=usuarioId,
                };

                var request = JsonConvert.SerializeObject(consultaSaldo);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/Saldoes/ConsultarSaldo";
                var response = await  client.PostAsync(url,content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se encontró saldo",
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var saldo = JsonConvert.DeserializeObject<Saldo>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Saldo Ok",
                    Result = saldo,
                };



            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result=null,
                    
                };
                throw;
            }
        }

        public async Task<Response> BuscarSaldo(string codigo, string usuarioId)
        {
            try
            {
                var buscarSaldoRequest = new BuscarTarjetaPrepago                {
                    Codigo = codigo,
                    UsuarioId = usuarioId,
                };

                var request = JsonConvert.SerializeObject(buscarSaldoRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/TarjetaPrepagoes/BuscarSaldo";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se encuentra la tarjeta prepago",
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var saldo = JsonConvert.DeserializeObject<Saldo>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Login Ok",
                    Result = saldo,
                };



            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }

        public async Task<List<T>> Get<T>(string controller) where T : class
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = string.Format("/api/{0}", controller);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return list.ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }

        

        public async Task<Response> NuevoCarro(Carro carro)
        {
            try
            {


                var request = JsonConvert.SerializeObject(carro);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/Carroes";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newCarro = JsonConvert.DeserializeObject<Carro>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Carro creado Ok",
                    Result = newCarro,
                };



            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }


        public async Task<List<Modelo>> GetModelos()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/Modeloes";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var modelos = JsonConvert.DeserializeObject<List<Modelo>>(result);

                return modelos;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<TarjetaCredito>> GetTarjetaCredito(string usuarioId)
        {
            try
            {
                var usuarioRequest = new UsuarioRequest
                {
                    UsuarioId = usuarioId,
                };


                var request = JsonConvert.SerializeObject(usuarioRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/TarjetasCreditoes/GetTarjetasCreditos";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var tarjetaCredito = JsonConvert.DeserializeObject<List<TarjetaCredito>>(result);

                return tarjetaCredito;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> NuevaTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            try
            {


                var request = JsonConvert.SerializeObject(tarjetaCredito);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/TarjetaCreditoes";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newTarjetaCredito = JsonConvert.DeserializeObject<TarjetaCredito>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Tarjeta de Crédito creada Ok",
                    Result = newTarjetaCredito,
                };



            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<Response> NuevaParqueo(Parqueo Parqueo)
        {
            try
            {


                var request = JsonConvert.SerializeObject(Parqueo);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/Parqueos/InsertarParqueo";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newParqueo= JsonConvert.DeserializeObject<Parqueo>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Parqueo  Ok",
                    Result = newParqueo,
                };



            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<List<Carro>> GetCarros(string usuarioId)
        {
            try
            {
                var usuarioRequest = new UsuarioRequest
                {
                    UsuarioId = usuarioId,
                  
                };

                var request = JsonConvert.SerializeObject(usuarioRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://QuitoParkApp.somee.com");
                var url = "/api/Carroes/GetCarros";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var carros = JsonConvert.DeserializeObject<List<Carro>>(result);

                return carros;

            }
            catch (Exception)
            {
                return null;
            }
        }

        //public static byte[] ReadFully(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        return ms.ToArray();
        //    }
        //}


        //public async Task<Response> SetPhoto(int customerId, Stream stream)
        //{
        //    try
        //    {
        //        var array = ReadFully(stream);

        //        var photoRequest = new PhotoRequest
        //        {
        //            Id = customerId,
        //            Array = array,
        //        };

        //        var request = JsonConvert.SerializeObject(photoRequest);
        //        var body = new StringContent(request, Encoding.UTF8, "application/json");
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("http://zulu-software.com");
        //        var url = "/ECommerce/api/Customers/SetPhoto";
        //        var response = await client.PostAsync(url, body);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = response.StatusCode.ToString(),
        //            };
        //        }

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = "Foto asignada Ok",
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message,
        //        };
        //    }

        //}

  }
}