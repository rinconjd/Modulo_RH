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

| Método | Ruta                  | Descripción                |
|--------|-----------------------|----------------------------|
| GET    | /api/empleados        | Listar todos los empleados |
| GET    | /api/empleados/{id}   | Obtener empleado por ID     |
| POST   | /api/empleados        | Crear nuevo empleado        |
| PUT    | /api/empleados/{id}   | Actualizar empleado         |
| DELETE | /api/empleados/{id}   | Eliminar empleado           |

---


