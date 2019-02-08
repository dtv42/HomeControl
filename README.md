# Home Control 2.2

## Introduction

The introduction of Windows 10 IoT running on the Raspberry PI enabled the
Windows platform for the implementation of all kind of interesting applications
using the familiar development environments such as Visual Studio and C#.
Now with the release of .NET Core 2.2, the Windows IoT platform supports web
application development out of the box requiring no custom solutions
(e.g. web server running as a background application).

### Home Automation

A typical scenario for a Raspberry PI based application is home automation.
Besides typical monitoring applications with a variety of sensors, the
integration of different devices such as ventilation units, power meters,
PV inverters, heating boilers, Z-Wave controls become an important issue.

## Problem

The various devices provide a wide set of application and user interfaces
ranging from web interfaces, REST based web API's to industrial Modbus TCP.
Some of the systems provide remote access to cloud based services requiring
different security support (two phase login, OAuth, custom secrets, passwords
etc.).

## Goal

The home control web application should provide a web interface for selected
data, the integration to cloud based monitoring services such as ThingSpeak,
and a consolidated REST based web API to simplify data access.
The Home Control Web Application gathers data from a various sources using
REST based Web API's or Modbus TCP:

- ETA PU 11 Pellets Boiler
- b-Control EM300-LR energy manager
- BMW Wallbox Connect
- Helios KWL EC 200 Ventilation Unit
- Fronius Symo Inverter
- Netatmo Weather Station and Modules
- Zipato Zipatile Z-Wave Home Control Unit

## Software Components

- Windows 10 IoT Core
- ASP.NET Core 2.2
- Serilog Logging Framework
- Modbus TCP (NModbus Library)
- Swagger (Swashbuckle.AspNetCore)
- Syncfusion (UI Controls)

### Windows 10 IoT Core

Windows IoT Core is a version of Windows 10 that is optimized for smaller
devices with or without a display that run on both ARM and x86/x64 devices.

### ASP.NET Core 2.2

With ASP.NET Core 2.2 the promise of cross platform development of various
application types (console applications, web applications etc.) become
even simpler. As it has been shown before, the deployment of ASP.NET web
applications to a Raspberry PI running Windows 10 IoT is pretty easy

1. just compile the web application for the win-arm run-time platform
2. and publish all to a directory on the Raspberry PI.

### Serilog

Serilog logging for ASP.NET Core is available with the Serilog.AspNetCore
package. This package routes ASP.NET Core log messages through Serilog,
combining information about ASP.NET's internal operations logged to the
same Serilog sinks as your application events.

### NModbus

NModbus is a C# implementation of the Modbus protocol and provides
connectivity to Modbus slave compatible devices and applications.
It supports serial ASCII, serial RTU, TCP, and UDP protocols.

### Swagger

Swashbuckle.AspNetCore is a Swagger tooling for API's built with ASP.NET Core.
It allows to generate API documentation, including a UI to explore and test
operations, directly from your routes, controllers and models.

In addition to its Swagger generator, Swashbuckle also provides an embedded
version of the wellkown swagger-ui that's powered by the generated Swagger JSON.

### Syncfusion

Syncfusion's Essential Studio Enterprise Edition is a suite of components and frameworks for developing web, mobile, and desktop applications.
Eligible developers and small businesses have free access to Syncfusion's
components, controls and frameworks for web, mobile, and desktop development.

## Implementation

The access to the various devices is implemented in custom libraries targeting
the .NET Standard 2.0. This allows the use of the libraries in different
applications (console application, Windows Universal application, ASP.NET
Web application). All the internal specifics of the data access is hidden,
exposing only the various data properties and read and (optional) write
operations.

- ETAPU11
- KWLEC200
- BControl
- EM300LR
- Fronius
- SYMO823M
- Netatmo
- Zipato
- NModbus
- DataValue
- BaseClass
- HomeControl (UWP)
- HomeMonitor (Xamarin)

### Libraries, Console, Web Applications and Apps

Typically for a particular device a .NET Standard 2.0 library is created,
providing all necessary communication and data fields to support the device.
A console application is provided to allow read and write operations
(if the device supports write access) for testing and convenience.
A Web application providing simple monitoring, a REST based API and
a SignalR based monitoring hub for all the device data.
A test project based on XUnit is used to test the various applications
and libraries.

#### Project Layout

Every device project has typically four projects:

- DeviceApp (a .NET Core 2.2 console application)
- DeviceWeb (an ASP.NET Core 2.2 web application)
- DeviceLib (the .NET Standard 2.0 device library)
- DeviceTest (a XUnit test application)

Some device project contains a simulation (console) application to simplify testing.
The Windows 10 UWP app uses the various Web applications to provide a simple common user interface.
The Xamarin based monitoring app is an example of a mobile application running on Android phones.

##### .NET Core 2.2 Console Application

The console application uses several utilities to provide a simple to use application framweork:

- CommandLine.Core.CommandLineUtils
- CommandLine.Core.Hosting
- Microsoft.AspNetCore
- Microsoft.Extensions.Configuration.Json
- Microsoft.Extensions.Configuration.EnvironmentVariables
- Newtonsoft.Json
- Serilog

The console application implements the Program class (providing the main entry point) and the Startup class implementing configuration and logging.
The device specific singleton instance (using a Modbus or HTTP client) is created.
The various commands (root, read, info, monitor etc.) implement the actual device operations provided by the device library.

All console applications are using a JSON configuration file containing common application settings ("appsettings.json").

##### .NET Standard 2.0 device library

The various devices have very different interfaces ranging from custom web services to Modbus TCP.

| Device / Service                   | Interface                            |
| ---------------------------------- |:------------------------------------ |
| ETA PU 11 Pellet Boiler            | Modbus TCP, REST Web Service (local) |
| Helios KWL EC 200 ventilation unit | Modbus TCP                           |
| b-control EM300 LR energy manager  | Modbus TCP, REST Web Service (local) |
| Fronius Symo 8-2-3M                | Modbus TCP, REST Web Service (local) |
| Netatmo Weather station            | REST Web Service (remote)            |
| Zipato Zipatile                    | REST Web Service (remote, local)     |

The various REST web services also have different authentication requirements.
All the access methods and the various data values are handled in their respective
.NET Standard 2.0 libraries.

The device library implements read (and optional write) operation for the observed device data values.
Depending on the device communication the library hides the actual protocol implementation.
The use of Modbus is simplified by using a Modbus TCP client (provided by a custom NModbus extension library).
In case of a device specific web service, the HTTP communication is provided by a HTTPClient instance.
The device specific rest web api's and associated security implementation (username, password, usersecrets etc.) simplifies the data access.

The constructor of the device class provided by the device library uses a logger, a client instance (Modbus or HTTP client), and device specific settings data.
The cached device data hold the device specific data values as properties.
The device data typically are derived from the DataValue helper class and implement the IPropertyHelper interface.

##### ASP.NET Core 2.2 web application

The web application uses the device specific library to implement a consolidated REST based web api for access to the (cached or updated) device data values.
The cached device data values are typically updated every minute utilizing a background timer task.
The updated data values are also provided by a SingalR based serivce propagating the update events.
The web application also provides a common web site with access to the data values (updated using SignalR) and a Swagger based web api test interface.

##### XUnit test application

The XUnit based test project typically implements tests for the device specific library, the web application, and the console application.

#### BaseClass

The *BaseClass* library provides a set of helper classes to facilitate the implementation of applications by providing standardized implementations in various contexts.
An application class is derived from the corresponding base class using the default implementation (constructor, properties, methods).

#### DataValue

In order to facilitate the access to the actual monitored (or controlled) data,
property set and get routines are added to allow a dot-notation for properties
supporting nested classes and simple arrays and lists.

#### Modbus

The devices providing a Modbus TCP interface are integrated using the NModbus
Library with several extensions for additional datatypes such as strings,
floats and double.  The Modbus TCP protocol is used only on the internal
network and is not exposed to the outside. This is also used for additional
tools such as a gerneric command line Modbus application or an Modbus TCP
gateway providing a REST based interface via HTTPS.

#### ASP.NET

The standard ASP.NET web application template using razor pages.
Several pages are added to display selected data from the various components.
For a more graphical overview the Syncfusion components are used to display circular gauges.
An additional page is used to embed the Swagger Web API.

#### SignalR

The SignalR communication framework in ASP.NET Core 2.2 is used for
propagating updated data values. It is used to allow the monitored data values
to be updated and displayed on the associated web pages automatically in the background.

#### Data Provider & Monitor Services

Simple singleton services provide the access to the instances for the actual
devices. The monitor services running in the background implement the
automatic update of monitored data typically every minute, accessing the devices
(Modbus TCP, Web API's etc.) and synchronizing the local cached data instances.

#### REST API and Swagger

The monitored data from the various devices are also made available via set
of consolidated REST based web API's. Swagger integration provided by the
Swashbuckle project adds Swagger to the WebApi project providing a rich
discovery, documentation and playground experience to the REST API consumers.

In order  to simplify the access to the monitor and control values a consistent set of REST based web services are implemented.
The typical REST interface is comprised of a simple set of functions:

~~~
GET /api/{device}/all - Returns all device data.

GET /api/{device}/overview - Returns a subset of the device data.

GET /api/{device}/property/{name} - Returns a single device property.

PUT /api/{device}/property/{name}?value={value} - Writes a single device property.
~~~

The varios subsets provided by the REST interface (and the underlying library) are used to prevent to update all data values (could be rather slow, depending on the device interface).
Note that only the ETAPU11, the KWLEC200, and the Zipato device support write operations.

#### Deployment

A Web application can be deployed on the various platforms supported by the
ASP.NET Core 2.2 framework. It also runs on a Raspberry PI 3 or Raspberry PI 2 B
using Windows 10 IoT Core by simply publishing to a directory on the Raspberry PI.
