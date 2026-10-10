# api-invoices

Descripción

Proyecto de ejemplo para gestión de facturas.

Arquitectura por capas

Se utilizó una arquitectura por capas para mantener el código organizado y con responsabilidades claramente separadas. Las capas principales y sus responsabilidades son:

- Presentación (API): Contiene los Controllers y los modelos de entrada/salida (DTOs). Se encarga de recibir las peticiones HTTP, efectuar validaciones básicas y delegar en la capa de Aplicación.
- Aplicación (Services / Use Cases): Orquesta los casos de uso de la aplicación (por ejemplo, crear una factura, listar facturas, gestionar usuarios). Define las interfaces (contratos) que necesita y aplica reglas de negocio de alto nivel; aquí se gestionan DTOs y mapeos.
- Dominio (Entidades y Reglas): Contiene las entidades principales (Invoice, Customer, User), objetos de valor, y las reglas de negocio puras. No depende de detalles de infraestructura.
- Infraestructura (Persistencia y Servicios externos): Implementaciones concretas de las interfaces de repositorio y servicios. Incluye el DbContext de Entity Framework Core, migraciones, seeding de datos y la implementación de la lógica de autenticación JWT y refresh tokens.

Patrones y buenas prácticas aplicadas

- Inversión de dependencias e inyección de dependencias (DI): Las capas superiores definen interfaces y las implementaciones concretas se inyectan desde Infraestructura.
- Repositorios / Unit of Work: Acceso a datos encapsulado en repositorios y uso del DbContext para transacciones.
- Mapeo entre entidades y DTOs: Uso de AutoMapper o mapeos manuales en la capa de Aplicación para separar modelos de persistencia de los modelos expuestos por la API.
- Validación y reglas de negocio: Validaciones de entrada en la capa de Presentación/Apliación y reglas invariantes en las entidades del Dominio.
- Tests: La separación facilita pruebas unitarias de Dominio y Aplicación, y pruebas de integración que involucren la Base de Datos y la Infraestructura.

Estructura de carpetas (ejemplo)

- src/Api (Controllers, Program.cs)
- src/Application (Services, DTOs, Interfaces)
- src/Domain (Entities, ValueObjects, Exceptions)
- src/Infrastructure (EF DbContext, Repositories, Implementaciones)

Tecnologías principales

- .NET 10
- Entity Framework Core (migrations y seed de datos)
- Autenticación con JWT (tokens de acceso) y refresh tokens

Base de datos

Entity Framework Core se utiliza para crear la base de datos y generar datos de prueba mediante migraciones y seeding. Ejecuta las migraciones para crear la BD local y los datos iniciales.

Autenticación

La aplicación implementa autenticación basada en JWT y refresh tokens. Al autenticarse se obtiene un token de acceso y un refresh token para renovar el acceso sin volver a ingresar credenciales.

Datos de prueba

Para facilitar las pruebas se incluyeron usuarios de ejemplo en el seeding:
- Usuario: admin
  Contraseña: Admin1234

Nota importante sobre configuración

Este repositorio es de prueba: para simplificar la evaluación se subieron las configuraciones necesarias incluyendo las que se encuentran en appsettings.json (por ejemplo cadenas de conexión y claves de JWT). No use estas configuraciones en producción. Revise y reemplace secretos y cadenas de conexión antes de desplegar.

Cómo probar (resumen rápido)

1. Restaurar paquetes y construir el proyecto.
2. Ejecutar las migrations de EF Core (dotnet ef database update) para crear la base de datos y seed de datos.
3. Iniciar la aplicación.
4. Usar el endpoint de autenticación (por ejemplo /api/auth/login) con los datos de prueba para obtener el token.
5. Probar refresh token usando el endpoint de refresh correspondiente (por ejemplo /api/auth/refresh).


