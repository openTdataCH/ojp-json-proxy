# 🚂 OJP-Explorer
This repository contains the frontend application `OJP-Explorer` for `OJP-Proxy`. OJP-Explorer is an interactive 
documentation meant to ease the getting-started process with the API provided by OJP-Proxy

## Repository

The repository follows these basic rules: 

| Folder        | Contains                                                              |
|---------------|-----------------------------------------------------------------------|
| `components`  | Building blocks of the application                                    | 
| `composables` | Services and helper functions                                         |
| `assets`      | Images, Icons, Fonts etc.                                             |
| `layouts`     | Main layouts for views                                                |
| `router`      | Manages routing inside the application, which url links to which view |
| `stores`      | Stores settings application wide                                      |
| `views`       | Main pages like home and doc                                          |
| `locales`     | All translations used in the application                              |

## env
To create a connection to OJP-Proxy in the env file the following connection strings have to be configured.

| Variable             | Explanation                                   | Example                       |
|----------------------|-----------------------------------------------|-------------------------------|
| VITE_API_BASEURL     | Base URL of the API                           | http://localhost:5047         | 
| VITE_SWAGGER_BASEURL | Base URL of the Swagger/OpenAPI Documentation | http://localhost:5047/swagger |

```
VITE_API_BASEURL=http://localhost:5047
VITE_SWAGGER_BASEURL=http://localhost:5047/swagger
```

## To consider
The main building blocks of the application get automatically generated from the swagger json of OJP-Proxy. This 
means a connection to the backend is needed in order to fully use the application.

# Instructions

## Project Setup
To install all dependencies run the following command: 

```sh
npm install
```

### Compile and Hot-Reload for Development
This command runs the application in dev mode meaning there is a Hot-Reload on changes:

```sh
npm run dev
```

### Type-Check, Compile and Minify for Production
To build the application for production run the following command.
This creates a `dist` folder with the built project.

```sh
npm run build
```

---
> built by Tobias Sturzenegger and Cédric Wagner @ [FHNW](https://www.fhnw.ch/en/)