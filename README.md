# QUICK-GUIDE - CLIENT GRPC

### 1- Clonar el proyecto correctamente.

### 2- Esperar que el proyecto termine de cargar, es importante que se prepare el ambiente.

### 3- Ejecutar el comando en la raiz del proyecto.

![image](https://github.com/user-attachments/assets/be7a1389-3bcd-4634-8c18-e84455f44022)

```bash
dotnet build
```
### 4- Asegurarse que el SERVIDOR este en ejecucion.

### 5- Iniciar cliente.

> Debe estar en HTTPS para que funcione el protocolo HTTP/2 correctamente.

![image](https://github.com/user-attachments/assets/2ecd9f8c-5963-4e8e-8d46-7e7f52a251d6)

### 6.1- Confirmar que el CLIENTE esta funcionando

> Se deberia poder ver la interfaz de swagger con los endpoints

![image](https://github.com/user-attachments/assets/02f062ee-b3e7-4ac1-8ea2-14fe36ddf317)

---

#### 6.2- Prueba de mensaje a traves de Grpc

> 1: Para poder probar el metodo

> 2: Agregar texto al parametro message
 
> 3: Ejecutar la prueba

> 4: La respuesta del servidor

![image](https://github.com/user-attachments/assets/92e19623-41e6-463e-98a3-d657a9445f90)

#### 6.3- Prueba de mensaje con persistencia SQL a traves de Grpc

> 1: Mismos pasos que el anterior pero en el metodo:

![image](https://github.com/user-attachments/assets/de4d3a30-f513-4fd8-abfe-f601b163f8a7)

> Asegurarse que el id sea el correcto en la base de datos (para corroborar que funcione)

![image](https://github.com/user-attachments/assets/efd7a787-820a-4686-be14-ce21c0450d2d)

> Si el id del usuario existe deberia ver esta respuesta o similar.

![image](https://github.com/user-attachments/assets/2371e8da-9232-4aa1-98c9-ac15309bf263)


