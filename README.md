# 🏥 GestionCitasApi

¡Hola! Este es mi proyecto para la asignatura de **Programación II en el ITLA**. Se trata de una **Web API** desarrollada en **.NET 9** utilizando **Controllers**, **Entity Framework Core** y **SQL Server**, diseñada para la gestión de pacientes y el agendamiento de citas médicas.

---

## 🚀 Tecnologías y Herramientas
* **Lenguaje:** C#
* **Framework:** ASP.NET Core Web API (.NET 9)
* **ORM:** Entity Framework Core
* **Base de Datos:** SQL Server (LocalDB)
* **Arquitectura:** Web API con Controladores, DTOs y DbContext

---

## 📌 Entidades y Relación
1. **`Paciente`**: Representa la entidad principal (Nombre, Teléfono).
2. **`Cita`**: Representa la entidad dependiente (Fecha, Motivo, PacienteId).
* **Relación:** Un paciente puede tener múltiples citas registradas (1 a N).

---

## 🛠️ Endpoints Disponibles

### 👤 Pacientes (`/api/pacientes`)
* `GET /api/pacientes` - Obtiene la lista completa de pacientes.
* `GET /api/pacientes/{id}` - Obtiene los detalles de un paciente específico.
* `POST /api/pacientes` - Registra un nuevo paciente.
* `PUT /api/pacientes/{id}` - Actualiza la información de un paciente.
* `DELETE /api/pacientes/{id}` - Elimina un paciente de la base de datos.

### 📅 Citas (`/api/citas`)
* `GET /api/citas` - Obtiene la lista de citas incluyendo el nombre del paciente.
* `GET /api/citas/{id}` - Obtiene los detalles de una cita específica.
* `POST /api/citas` - Agenda una nueva cita asociándola a un `PacienteId`.
* `PUT /api/citas/{id}` - Actualiza la información de una cita.
* `DELETE /api/citas/{id}` - Cancela/elimina una cita.

---

## 💻 Autor
* **Sheila Yohanny Severino** — *Estudiante de Desarrollo de Software (ITLA)*
