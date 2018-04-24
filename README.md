# WebApplicationLatch
Aplicación WEB de aspnet integrada con LATCH de Elevenpaths

En el area de desarrolladores de la aplicación LATCH (https://latch.elevenpaths.com/www/developers/) 
he creado una aplicación llamada (aplicacion1) con dos operaciones:

1.- Login La operación "Login" bloquea el acceso a la aplicación cuando el LATCH de esta operación está activo (on). 
De manera que para acceder a la aplicación el LATCH de "Login" debe estar en (off).

2.- ChangePassword La operación "ChangePassword" bloquea el cambio de contraseña de un usuario que haya iniciado 
sesión en la aplicación cuando el LATCH de esta operación está activo (on).
