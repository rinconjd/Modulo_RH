# 📅 Fecha
4 de Abril de 2025

# 👨‍💻 Autores

| Nombre               | Foto |
|----------------------|------|
| **Juan David Rincón** | <img src="https://github.com/user-attachments/assets/b54a095e-bd7c-4e3f-b383-b6e8e0977e52" width="150"/> |
| **Julián Rodriguez** | <img src="https://github.com/user-attachments/assets/afdfeff6-8865-433a-8ed8-89503c0c6e2d" width="150"/> |

# 📌 Modulo RH - Proyecto de arquitectura de Software
Este módulo permite la gestión de datos del personal dentro de la plataforma Retail para empleados. Incluye funcionalidades como consulta de información personal, historial laboral, novedades (ausencias, incapacidades), y actualización de datos.

El objetivo principal fue ofrecer una solución centralizada, accesible vía servicios web, que facilitara la integración entre el sistema de Recursos Humanos y otras plataformas internas, garantizando disponibilidad, trazabilidad y control de cambios.

## 🧰 Tecnologías usadas

Lenguaje / Framework principal:
1. C# con .NET 9.0.2 (ASP.NET Core Web API)

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express
- Swagger UI
- Visual Studio / .NET CLI

---

## 🚀 Requisitos previos

- [.NET SDK 7 o superior](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express o completo](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio 2022+ o cualquier editor con soporte para .NET
- SQL Server Management Studio (SSMS) o SQL Server Object Explorer

---

## ⚙️ Configuración del proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/usuario/proyecto-rh.git
cd proyecto-rh/Modulo_RH
```

### 2. Instalar dependencias

```bash
dotnet restore
```

### 3. Configurar la cadena de conexión

Edita el archivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=RH_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> ⚠️ Ajusta `localhost\SQLEXPRESS` si tu instancia de SQL Server es diferente.

---

## 🔐 Clave secreta JWT

El proyecto utiliza autenticación basada en JWT (JSON Web Tokens) para proteger los endpoints. Para que el sistema genere y valide correctamente los tokens, es necesario definir una clave secreta robusta en el archivo `appsettings.json`:

```json
"JwtSettings": {
  "SecretKey": "TU_CLAVE_SECRETA_DE_32+_CARACTERES",
  "Issuer": "ModuloRH",
  "Audience": "ModuloRHUsuarios"
}
```

### ✅ Recomendaciones:

- La `SecretKey` debe tener **mínimo 32 caracteres** para ser compatible con el algoritmo `HS256`.
- Nunca uses claves débiles como `"123"` o `"clave"`.
- **No subas la clave real a GitHub.** Agrega `appsettings.json` a tu `.gitignore` y proporciona un `appsettings.Development.json` como plantilla de ejemplo.

---

### 🧺 ¿Cómo usar el token en Swagger?

1. Haz una petición `POST /api/login` con:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

2. Copia el `token` recibido.

3. En Swagger UI, haz clic en el botón **Authorize 🔒**.

4. Escribe el token así:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

5. Ahora podrás consumir todos los endpoints protegidos como administrador.

---

## 🧱 Base de datos

### 4. Crear la base de datos (usando EF Core)

Instala la herramienta si no la tienes:

```bash
dotnet tool install --global dotnet-ef
```

Aplica la migración inicial:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🛠️ Ejecutar la aplicación

```bash
dotnet run
```

Abre en el navegador:

```
https://localhost:<puerto>/swagger
```

Desde Swagger UI podrás probar todos los endpoints REST del CRUD.

---

## 📦 Endpoints disponibles

| Método | Ruta                         | Descripción                             |
|--------|------------------------------|-----------------------------------------|
| POST   | /api/login                   | Obtener token de autenticación          |
| GET    | /api/empleados               | Listar todos los empleados              |
| GET    | /api/empleados/{id}          | Obtener empleado por ID                 |
| GET    | /api/empleados/filtrar       | Filtrar empleados por múltiples campos  |
| POST   | /api/empleados               | Crear nuevo empleado                    |
| PUT    | /api/empleados/{id}          | Actualizar empleado                     |
| DELETE | /api/empleados/{id}          | Eliminar empleado                       |

---
