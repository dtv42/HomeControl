# NModbusRTU

The *NModbusRTU* project is a sample implementation of a Modbus Gateway using a simple REST based interface to access Modbus RTU slave devices.
The implementation uses ASP.NET core 2.2, Serilog, and Swagger (Swashbuckle).

## ASP.NET Core 2.2

ASP.NET Core is an open-source and cross-platform framework for building modern cloud based internet connected applications, such as web apps, IoT apps and mobile backends. ASP.NET Core apps can run on .NET Core or on the full .NET Framework. It was architected to provide an optimized development framework for apps that are deployed to the cloud or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your solutions. You can develop and run your ASP.NET Core apps cross-platform on Windows, Mac and Linux.
[Learn more about ASP.NET Core](https://docs.microsoft.com/aspnet/core/).

## Swagger

When consuming a Web API, understanding its various methods can be challenging for a developer. Swagger, also known as [OpenAPI](https://www.openapis.org/), solves the problem of generating useful documentation and help pages for Web APIs. It provides benefits such as interactive documentation, client SDK generation, and API discoverability.

### Swagger specification (swagger.json)

The Swagger specification — by default, is a generated document named *swagger.json* based on the REST API. It describes the capabilities of the API and how to access it with HTTP. It drives the Swagger UI and is used by the tool chain to enable discovery and client code generation. 

~~~JSON
{
  "swagger": "2.0",
  "info": {
    "version": "v1",
    "title": "NModbusRTU Web API",
    "description": "This is a web gateway service for Modbus RTU slave devices.",
    "termsOfService": "/Terms",
    "contact": {
      "name": "Dr. Peter Trimmel",
      "url": "http://dtv-online.net",
      "email": "peter.trimmel@live.com"
    },
    "license": {
      "name": "Use under the MIT license",
      "url": "https://opensource.org/licenses/MIT/"
    }
  },
  "paths": {
    "/api/Coil/{offset}": {
      "get": { ... },
      ...
      "put": { ... }
    },
    ...
  }
}
~~~

### Swagger UI

Swagger UI offers a web-based UI that provides information about the service, using the generated Swagger specification. Both Swashbuckle and NSwag include an embedded version of Swagger UI, so that it can be hosted in your ASP.NET Core app using a middleware registration call.

### Swashbuckle

Swashbuckle.AspNetCore is an open source project for generating Swagger documents for ASP.NET Core Web APIs.

## Implementation

The ASP.NET Core 2.2 template for Web application (razor pages and MVC ontrollers) is used and augmented to include logging (using Serilog) and individual authentication.
Using HTTPS to access the Web pages (and REST API) a secure layer is added accessing Modbus RTU slave data.
The application settings stored in the *appsettings.json* file allow to configure the application.

~~~JSON
{
  "AppSettings": {
    "RtuMaster": {
      "SerialPort": "COM1",
      "Baudrate": 19200,
      "ReadTimeout": 10000,
      "WriteTimeout": 10000
    },
    "RtuSlave": {
      "ID": 1
    },
    "Swagger": {
      "Info": {
        "Title": "NModbusRTU Web API",
        "Description": "This is a web gateway service for Modbus RTU slave devices.",
        "TermsOfService": "/Terms",
        "Contact": {
          "Name": "Dr. Peter Trimmel",
          "Email": "peter.trimmel@live.com",
          "Url": "http://dtv-online.net"
        },
        "License": {
          "Name": "Use under the MIT license",
          "Url": "https://opensource.org/licenses/MIT/"
        }
      }
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "DataSource=NModbusRTU.db"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "RollingFile",
        "Args": {
          "restrictedToMinimumLevel": "Information",
          "outputTemplate": "[{Timestamp:HH:mm:ss}] [{SourceContext}] [{Level}] {Message}{NewLine}{Exception}",
          "pathFormat": "Logs\\log-{Date}.log",
          "retainedFileCountLimit": 10
        }
      }
    ]
  },
  "AllowedHosts": "*"
}
~~~

The *NModbusRTU* Web Application allows to read and write data from Modbus RTU slaves using REST based Web API's.

- Coils (read write)
- Discrete Inputs (readonly)
- Holding Registers (read write)
- Input Registers (readonly)
- All Modbus data are accessible through a common Web API documented using Swagger.

Additionally various additional data types are supported.

Readonly data are mapped to input registers or discrete inputs.

| Operation | Description |
|-----------|-------------|
| ReadOnlyString | Reads an ASCII string (multiple input registers)
| ReadOnlyHexString | Reads an HEX string (multiple input registers)
| ReadOnlyBool | Reads a boolean value (single discrete input)
| ReadOnlyBits | Reads a 16-bit bit array value (single input register)
| ReadOnlyShort | Reads a 16 bit integer (single input register)
| ReadOnlyUShort | Reads an unsigned 16 bit integer (single input register)
| ReadOnlyInt32 | Reads a 32 bit integer (two input registers)
| ReadOnlyUInt32 | Reads an unsigned 32 bit integer (two input registers)
| ReadOnlyFloat | Reads a 32 bit IEEE floating point number (two input registers)
| ReadOnlyDouble | Reads a 64 bit IEEE floating point number (four input registers)
| ReadOnlyLong | Reads a 64 bit integer (four input registers)
| ReadOnlyULong | Reads an unsigned 64 bit integer (four input registers)

Arrays of datatypes are supported.

| Operation | Description |
|-----------|-------------|
| ReadOnlyBoolArray | Reads an array of boolean values (multiple discrete inputs)
| ReadOnlyBytes | Reads 8 bit values (multiple input register)
| ReadOnlyShortArray | Reads an array of 16 bit integers (multiple input register)
| ReadOnlyUShortArray | Reads an array of unsigned 16 bit integer (multiple input register)
| ReadOnlyInt32Array | Reads an array of 32 bit integers (multiple input registers)
| ReadOnlyUInt32Array | Reads an array of unsigned 32 bit integers (multiple input registers)
| ReadOnlyFloatArray | Reads an array of 32 bit IEEE floating point numbers (multiple input registers)
| ReadOnlyDoubleArray | Reads an array of 64 bit IEEE floating point numbers (multiple input registers)
| ReadOnlyLongArray | Reads an array of 64 bit integers (multiple input registers)
| ReadOnlyULongArray | Reads an array of unsigned 64 bit integers (multiple input registers)

Read and write data are mapped to holding registers or coils.

| Operation | Description |
|-----------|-------------|
| ReadString | Reads an ASCII string (multiple holding registers)
| ReadHexString | Reads an HEX string (multiple holding registers)
| ReadBool | Reads a boolean value (single coil)
| ReadBits | Reads a 16-bit bit array value (single holding register)
| ReadShort | Reads a 16 bit integer (single holding register)
| ReadUShort | Reads an unsigned 16 bit integer (single holding register)
| ReadInt32 | Reads a 32 bit integer (two holding registers)
| ReadUInt32 | Reads an unsigned 32 bit integer (two holding registers)
| ReadFloat | Reads a 32 bit IEEE floating point number (two holding registers)
| ReadDouble | Reads a 64 bit IEEE floating point number (four holding registers)
| ReadLong | Reads a 64 bit integer (four holding registers)
| ReadULong | Reads an unsigned 64 bit integer (four holding registers)
| WriteString | Writes an ASCII string (multiple holding registers)
| WriteHexString | Writes an HEX string (multiple holding registers)
| WriteBool | Writes a boolean value (single coil)
| WriteBits | Writes a 16-bit bit array value (single holding register)
| WriteShort | Writes a 16 bit integer (single holding register)
| WriteUShort | Writes an unsigned 16 bit integer (single holding register)
| WriteInt32 | Writes a 32 bit integer (two holding registers)
| WriteUInt32 | Writes an unsigned 32 bit integer (two holding registers)
| WriteFloat | Writes a 32 bit IEEE floating point number (two holding registers)
| WriteDouble | Writes a 64 bit IEEE floating point number (four holding registers)
| WriteLong | Writes a 64 bit integer (four holding registers)
| WriteULong | Writes an unsigned 64 bit integer (four holding registers)

Arrays of datatypes are supported.

| Operation | Description |
|-----------|-------------|
| ReadBoolArray | Reads an array of boolean values (multiple coils)
| ReadBytes | Reads 8 bit values (multiple holding register)
| ReadShortArray | Reads an array of 16 bit integers (multiple holding register)
| ReadUShortArray | Reads an array of unsigned 16 bit integer (multiple holding register)
| ReadInt32Array | Reads an array of 32 bit integers (multiple holding registers)
| ReadUInt32Array | Reads an array of unsigned 32 bit integers (multiple holding registers)
| ReadFloatArray | Reads an array of 32 bit IEEE floating point numbers (multiple holding registers)
| ReadDoubleArray | Reads an array of 64 bit IEEE floating point numbers (multiple holding registers)
| ReadLongArray | Reads an array of 64 bit integers (multiple holding registers)
| ReadULongArray | Reads an array of unsigned 64 bit integers (multiple holding registers)
| WriteBoolArray | Writes an array of boolean values (multiple coils)
| WriteBytes | Writes 8 bit values (multiple holding register)
| WriteShortArray | Writes an array of 16 bit integers (multiple holding register)
| WriteUShortArray | Writes an array of unsigned 16 bit integer (multiple holding register)
| WriteInt32Array | Writes an array of 32 bit integers (multiple holding registers)
| WriteUInt32Array | Writes an array of unsigned 32 bit integers (multiple holding registers)
| WriteFloatArray | Writes an array of 32 bit IEEE floating point numbers (multiple holding registers)
| WriteDoubleArray | Writes an array of 64 bit IEEE floating point numbers (multiple holding registers)
| WriteLongArray | Writes an array of 64 bit integers (multiple holding registers)
| WriteULongArray | Writes an array of unsigned 64 bit integers (multiple holding registers)
