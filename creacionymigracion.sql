use GD1C2016

/* 
creacion de schema*/

go
go
CREATE SCHEMA [GROUP_APROVED] AUTHORIZATION [gd]


go

/*CREACION DE TABLAS*/
begin transaction createTables
CREATE TABLE GROUP_APROVED.Funciones (
	Id_Func INT PRIMARY KEY IDENTITY(1,1),
	Desc_Func nvarchar,
	Estado char check(Estado IN ('H','I')) default('H'),

)

CREATE TABLE GROUP_APROVED.Roles (
	Id_Rol int IDENTITY(1,1) PRIMARY KEY,
	Desc_Rol nvarchar(255),					/*usar el desc_rol para verificar en que tabla, cliente o empresa , hacer el select mas tarde */
	Estado char check(Estado IN ('H','I')) default('H'),
)

CREATE TABLE GROUP_APROVED.FuncionesxRol (
	Id_Rol INT REFERENCES GROUP_APROVED.Roles,
	Id_Func INT REFERENCES GROUP_APROVED.Funciones,
	PRIMARY KEY (Id_Rol,Id_Func),
)

CREATE TABLE GROUP_APROVED.Usuarios (
	Id_Usr INT IDENTITY(0,1) PRIMARY KEY,
	Username nvarchar(255) UNIQUE,
	Passw nvarchar(255),
	Fecha_Creacion datetime default(getdate()),
	intentos smallint,
	Estado char check(Estado IN ('H','I','B')) default('H'),     /*H habilitado I inhabilitado B baja logica*/
)



CREATE TABLE GROUP_APROVED.RolesxUsuario (
	Id_Usr INT REFERENCES GROUP_APROVED.Usuarios,
	Id_Roles int REFERENCES GROUP_APROVED.Roles,
	PRIMARY KEY (Id_Usr, Id_Roles),
)



CREATE TABLE GROUP_APROVED.Empresas (
	Empresa_Razon_Social nvarchar(100),
	Empresa_Cuit nvarchar(100),
	Empresa_Mail nvarchar(255),																			/*VERIFICAR FECHA CREACION EMPRESA, NO ES LO MISMO Q FECHA CREACION USUARIO (?)*/
	Empresa_Dom_Calle nvarchar (255),
	Empresa_Nro_Calle numeric(18,0),
	Empresa_Piso numeric(18,0),
	Empresa_Depto nvarchar(50),
	Empresa_Fecha_Creacion datetime,
	Empresa_Cod_Postal nvarchar(255),
	Id_Usuario INT REFERENCES GROUP_APROVED.Usuarios,
	Empresa_Telefono numeric(18,0),
	Empresa_Nombre_Contacto nvarchar(255),
	Empresa_RubroP nvarchar(255),
	PRIMARY KEY (Empresa_Razon_Social, Empresa_Cuit)

)




CREATE TABLE GROUP_APROVED.Clientes (
	Dni_Cli numeric(18,0),
	Tipo_Dni nvarchar(7) check(Tipo_Dni IN('DNI','LE','LC','DNIEXT')) default ('DNI'),
	Cli_Nombre  nvarchar(255),
	Cli_Apellido nvarchar(255),
	Cli_Fecha_Nac datetime,
	CLI_Telefono numeric(18,0),
	Cli_Mail nvarchar(255),
	Cli_Dom_Calle nvarchar(255),
	Cli_Nro_Calle numeric(18,0),
	Cli_Piso numeric(18,0),
	Cli_Depto nvarchar(50),
	Cli_Cod_Postal nvarchar(255),
	Id_Usuario INT REFERENCES GROUP_APROVED.Usuarios,
	PRIMARY KEY ( Dni_Cli, Tipo_Dni)
)


CREATE TABLE GROUP_APROVED.Visibilidades(
	Visibilidad_Cod numeric(18,0) PRIMARY KEY,
	Visibilidad_Desc nvarchar(255),
	Visibilidad_Precio numeric(18,2),
	Visibilidad_Porcentaje numeric(18,2),
	
)


CREATE TABLE GROUP_APROVED.Rubros(
	Id_Rubro numeric(18,0) IDENTITY(0,1) PRIMARY KEY,
	Rubro_Desc_Corta nvarchar(255),
	Rubro_Desc_Completa nvarchar(255)

)

CREATE TABLE GROUP_APROVED.Estado_Publ(
	Id_Est INT IDENTITY(0,1) PRIMARY KEY,
	Descripcion nvarchar(255) CHECK ( Descripcion IN('Borrador','Activa','Pausada','Finalizada'))
)




CREATE TABLE GROUP_APROVED.Publicaciones(
	Publicacion_Cod INT IDENTITY(12353,1) PRIMARY KEY,      
	Publicacion_Desc nvarchar(255),
	Publicacion_Stock numeric(18,0),
	Publicacion_Fecha datetime,
	Publicacion_Fecha_Venc datetime,
	Publicacion_Precio numeric(18,2),
	Publicacion_Tipo nvarchar(255),  
	Visibilidad_Cod numeric(18,0) REFERENCES GROUP_APROVED.Visibilidades,
	Publicacion_Estado INT REFERENCES GROUP_APROVED.Estado_Publ,
	Id_Rubro numeric(18,0) REFERENCES GROUP_APROVED.Rubros,
	Id_Usuario INT REFERENCES GROUP_APROVED.Usuarios,

)


/*go
SET IDENTITY_INSERT GROUP_APROVED.Publicaciones ON;      /* esto es para poder migrar los datos inciales en orden, sin porblemas, despues de migrar se vuelve a setear a off*/
go*/

CREATE TABLE GROUP_APROVED.Ofertas (												/* se debe restringir que solo las publicaicones de tipo subasta tienen ofertas asignadas*/
	ID_Oferta numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Oferta_Fecha datetime,
	Oferta_Monto numeric(18,2),
	Id_Usuario INT REFERENCES GROUP_APROVED.Usuarios,
	Publicacion_Cod INT REFERENCES GROUP_APROVED.Publicaciones
	
)

CREATE TABLE GROUP_APROVED.Compras(
	ID_Compra numeric(18,0) IDENTITY(1,1) PRIMARY KEY,
	Compra_Fecha datetime,
	Compra_Cantidad numeric(18,2),
	Id_Usuario INT REFERENCES GROUP_APROVED.Usuarios,
	Publicacion_Cod INT REFERENCES GROUP_APROVED.Publicaciones

)

CREATE TABLE GROUP_APROVED.Calificaciones(
	Calif_Cod numeric(18,0) PRIMARY KEY,
	Calif_Cant_Est numeric(18,0),
	Calif_Descr nvarchar(255),
	ID_Compra numeric(18,0) REFERENCES GROUP_APROVED.Compras,       /*el usuario que hizo la calificacion se consigue de la compra*/

)


CREATE TABLE GROUP_APROVED.Facturas (
	Nro_Fact numeric(18,0) PRIMARY KEY,
	Fact_Fecha datetime,
	Fact_Total numeric(18,2),
	Fact_Forma_Pago nvarchar(255),
	Publicacion_Cod INT REFERENCES GROUP_APROVED.Publicaciones,


)

CREATE TABLE GROUP_APROVED.Items(
	Nro_Fact numeric(18,0) REFERENCES GROUP_APROVED.Facturas,
	Nro_item numeric(18,0),
	Item_Monto numeric(18,2),
	Item_Cantidad numeric(18,0),
	Item_Tipo varchar(255) default('publicacion'),                                    /*tipo de item, por venta, por envio por publicacion*/
	PRIMARY KEY( Nro_Fact , Nro_item)
)
commit transaction createTables



go

/*DROP DE TABLAS EN ORDEN CORRECTO
begin transaction dropTables
	
	DROP TABLE GROUP_APROVED.Items
	DROP TABLE GROUP_APROVED.Facturas
	DROP TABLE GROUP_APROVED.Calificaciones
	DROP TABLE GROUP_APROVED.Ofertas
	DROP TABLE GROUP_APROVED.Compras
	DROP TABLE GROUP_APROVED.Publicaciones
	DROP TABLE GROUP_APROVED.Estado_Publ
	DROP TABLE GROUP_APROVED.Visibilidades
	DROP TABLE GROUP_APROVED.Rubros
	DROP TABLE GROUP_APROVED.Clientes
	DROP TABLE GROUP_APROVED.Empresas
	DROP TABLE GROUP_APROVED.RolesxUsuario
	DROP TABLE GROUP_APROVED.Usuarios
	DROP TABLE GROUP_APROVED.FuncionesxRol
	DROP TABLE GROUP_APROVED.Roles
	DROP TABLE GROUP_APROVED.Funciones
	DROP SCHEMA [GROUP_APROVED]
commit transaction
*/
go
/*CREACION DE STORED PROCEDURES / TRIGGERS*/



-- select distinct Cli_Dni,Cli_Nombre,Cli_Apeliido from gd_esquema.Maestra


	Create Procedure GROUP_APROVED.usrCreationCli

as

begin
	declare usrCli cursor for
	select distinct Dni_Cli,Cli_Nombre, Cli_Apellido  from GROUP_APROVED.Clientes

	declare @Dni numeric(18,0);
	declare @Nombre nvarchar(255), @Apellido nvarchar(255), @usr nvarchar(255), @passw nvarchar(255);
	declare @RolCli int;
	set @RolCli = (select Id_Rol from GROUP_APROVED.Roles where Desc_Rol = 'Cliente')

	open usrCli;
	fetch next from usrCli into @Dni,@Nombre, @Apellido

	while @@FETCH_STATUS =0
		begin
			set @usr = concat(substring(@Nombre,1,1),substring(@Apellido,1,3)) 	;     /*creacion de usuario a partir de primera letra del nombre y primeras 3 letras del ape ape (?)*/

			set @passw = HASHBYTES('SHA2_256',concat(substring(@Apellido,1,4),substring(cast(@Dni as nvarchar(255)),2,3)));    /* creacion de passw a partir de las primeras 4 letras del apellido y las primeras 3 numeros del dni, excluyendo el primer numero */

			insert into GROUP_APROVED.Usuarios (Username, Passw, Fecha_Creacion)						
			values(@usr,@passw,getdate());
			
			set @usr = (select Id_Usr from GROUP_APROVED.Usuarios where Username = @usr);

			insert into GROUP_APROVED.RolesxUsuario(Id_Usr,Id_Roles)
			values(@usr,@RolCli);


			update GROUP_APROVED.Clientes
			set Id_Usuario = @usr
			where Dni_Cli = @Dni;
			
			
			fetch next from usrCli into @Dni,@Nombre, @Apellido
		end;
	
	close usrCli;
	deallocate usrCli;

end;
	go


  /*drop procedure usrCreationCli*/

	go

	CREATE Procedure GROUP_APROVED.usrCreationEmp

as

begin
	declare usrCli cursor for
	select distinct Empresa_Razon_Social,Empresa_Cuit from GROUP_APROVED.Empresas

	declare @Cuit nvarchar(100);
	declare @RazonS nvarchar(100), @usr nvarchar(255), @passw nvarchar(255);
	declare @RolEmp int;
	set @RolEmp = (select Id_Rol from GROUP_APROVED.Roles where Desc_Rol = 'Empresa')

	open usrCli;
	fetch next from usrCli into @Razons, @Cuit

	while @@FETCH_STATUS =0
		begin
			set @usr = concat(substring(@RazonS,17,2),substring(@Cuit,4,5)) 	;     /*creacion de usuario a partir de los ultimos 2 numeros de la razon social y 5 numeros deespues de los primeros 3 del cuit (?)*/

			set @passw = HASHBYTES('SHA2_256',concat(substring(@RazonS,1,4),substring(@Cuit,2,3)));    /* creacion de passw a partir de las primeras 4 letras del apellido y las primeras 3 numeros del dni, excluyendo el primer numero */

			insert into GROUP_APROVED.Usuarios (Username, Passw, Fecha_Creacion)						
			values(@usr,@passw,getdate());
			
			set @usr = (select Id_Usr from GROUP_APROVED.Usuarios where Username = @usr);

			insert into GROUP_APROVED.RolesxUsuario(Id_Usr,Id_Roles)
			values(@usr,@RolEmp);


			update GROUP_APROVED.Empresas
			set Id_Usuario = @usr
			where Empresa_Razon_Social = @RazonS and Empresa_Cuit = @Cuit
			
			
			fetch next from usrCli into @Razons, @Cuit
		end;
	
	close usrCli;
	deallocate usrCli;

end;

		
	go


   /*drop procedure usrCreationEmp*/



CREATE PROCEDURE GROUP_APROVED.funcionesAdmin

as

begin 
	declare @Id_Rol INT, @ID_Func INT;
	set @Id_Rol = (select Id_Rol from GROUP_APROVED.Roles where Desc_Rol = 'Administrador');
	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'r');
	
	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);
	

	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'b');

	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);


end;

go
	
	/*drop procedure funcionesAdmin*/

CREATE PROCEDURE GROUP_APROVED.funcionesCliente

as
	
begin 
	declare @Id_Rol INT, @ID_Func INT;
	set @Id_Rol = (select Id_Rol from GROUP_APROVED.Roles where Desc_Rol = 'Cliente');
	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'p');
	
	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);
	

	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'f');

	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);

	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'c');

	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);



end;
go


	/*drop procedure funcionesCliente*/

CREATE PROCEDURE GROUP_APROVED.funcionesEmpresa

as

begin 
	declare @Id_Rol INT, @ID_Func INT;
	set @Id_Rol = (select Id_Rol from GROUP_APROVED.Roles where Desc_Rol = 'Empresa');
	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'p');
	
	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);
	

	set @ID_Func = ( select Id_Func from GROUP_APROVED.Funciones where Desc_Func = 'f');

	insert into GROUP_APROVED.FuncionesxRol(Id_Rol,Id_Func)
	values(@Id_Rol,@ID_Func);



end;

go
/*drop procedure funcionesEmpresa*/

create procedure GROUP_APROVED.migracionPubl

as
begin

	
 declare @ID_usr INT;
 declare @Publ_Cod numeric(18,0) , @Publ_Cli_Dni numeric(18,0),@Publ_Empresa_Razon_Social nvarchar(255), @Publ_Empresa_Cuit nvarchar(50), @Publicacion_Rubro_Descripcion nvarchar(255),@Publicacion_Visibilidad_Cod numeric(18,0),@Publicacion_Descripcion nvarchar(255), @Publicacion_Fecha datetime, @Publicacion_Fecha_Venc datetime, @Publicacion_Precio numeric(18,2), @Publicacion_Stock numeric(18,0),@Publicacion_Tipo nvarchar(255); 
 declare @Id_Pub_Estado INT,@Id_Rubro INT;
 declare publCursr cursor for
	select distinct Publicacion_Cod, Publ_Cli_Dni, Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publicacion_Rubro_Descripcion,Publicacion_Visibilidad_Cod,Publicacion_Descripcion, Publicacion_Fecha, Publicacion_Fecha_Venc, Publicacion_Precio, Publicacion_Stock, Publicacion_Tipo
	from gd_esquema.Maestra order by 1;

open publCursr;
fetch next from publCursr into @Publ_Cod,@Publ_Cli_Dni,@Publ_Empresa_Razon_Social,@Publ_Empresa_Cuit, @Publicacion_Rubro_Descripcion,@Publicacion_Visibilidad_Cod,@Publicacion_Descripcion,@Publicacion_Fecha,@Publicacion_Fecha_Venc ,@Publicacion_Precio ,@Publicacion_Stock ,@Publicacion_Tipo;
while @@FETCH_STATUS =0
	begin
		
		
		if @Publicacion_Tipo = 'Compra Inmediata'
			begin
				if @Publicacion_Fecha_Venc < getdate()
					begin
						select @Id_Pub_Estado = Id_Est from GROUP_APROVED.Estado_Publ WHERE  Descripcion ='Finalizada';	
					end;
			end;
		else
			begin
				select @Id_Pub_Estado = Id_Est from GROUP_APROVED.Estado_Publ WHERE  Descripcion ='Activa';
			end;
	
		select @Id_Rubro = Id_Rubro from GROUP_APROVED.Rubros WHERE Rubro_Desc_Completa = @Publicacion_Rubro_Descripcion

		if @Publ_Cli_Dni <>0
			begin
			select @ID_usr = Id_Usuario from GROUP_APROVED.Clientes where Dni_Cli = @Publ_Cli_Dni
			
			insert into GROUP_APROVED.Publicaciones(Publicacion_Desc,Publicacion_Stock,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,Publicacion_Tipo,Visibilidad_Cod,Publicacion_Estado,Id_Rubro,Id_Usuario)

			values(@Publicacion_Descripcion,@Publicacion_Stock, @Publicacion_Fecha, @Publicacion_Fecha_Venc, @Publicacion_Precio,@Publicacion_Tipo,@Publicacion_Visibilidad_Cod,@Id_Pub_Estado,@Id_Rubro,@ID_usr)
			
			end;
		else
			begin
			select @ID_usr = Id_Usuario from GROUP_APROVED.Empresas where Empresa_Razon_Social = @Publ_Empresa_Razon_Social and Empresa_Cuit = @Publ_Empresa_Cuit
			
			insert into GROUP_APROVED.Publicaciones(Publicacion_Desc,Publicacion_Stock,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,Publicacion_Tipo,Visibilidad_Cod,Publicacion_Estado,Id_Rubro,Id_Usuario)

			values(@Publicacion_Descripcion,@Publicacion_Stock, @Publicacion_Fecha, @Publicacion_Fecha_Venc, @Publicacion_Precio,@Publicacion_Tipo,@Publicacion_Visibilidad_Cod,@Id_Pub_Estado,@Id_Rubro,@ID_usr)
			
			end;
		fetch next from publCursr into @Publ_Cod,@Publ_Cli_Dni,@Publ_Empresa_Razon_Social,@Publ_Empresa_Cuit, @Publicacion_Rubro_Descripcion,@Publicacion_Visibilidad_Cod,@Publicacion_Descripcion,@Publicacion_Fecha,@Publicacion_Fecha_Venc ,@Publicacion_Precio ,@Publicacion_Stock ,@Publicacion_Tipo;
	end;

	close publCursr;
	deallocate publCursr;

end;



go
Create procedure GROUP_APROVED.migrComprasCalif
as
begin 
	declare @Calificacion_Codigo numeric(18,0), @Calificacion_Cant_Estrellas numeric(18,1), @Compra_Cantidad numeric(18,0), @Publicacion_Cod numeric(18,0)
	declare @Calificacion_Descripcion nvarchar(255);
	declare @Compra_Fecha datetime;
	declare @Id_Usr INT;
	declare @Cli_Dni numeric(18,0);
	declare cursorCompCalif cursor for
	select Calificacion_Codigo, Calificacion_Cant_Estrellas, Calificacion_Descripcion, Compra_Fecha, Compra_Cantidad,Publicacion_Cod, Cli_Dni from gd_esquema.Maestra  where Compra_Fecha is not null;

	open cursorCompCalif;

	fetch next from cursorCompCalif into @Calificacion_Codigo , @Calificacion_Cant_Estrellas, @Calificacion_Descripcion, @Compra_Fecha, @Compra_Cantidad, @Publicacion_Cod, @Cli_Dni;

	while @@FETCH_STATUS = 0
	begin
		
		set @Id_Usr = (select Id_Usuario from GROUP_APROVED.Clientes where Dni_Cli = @Cli_Dni );

		if @Calificacion_Codigo is not null
			begin 
				insert into GROUP_APROVED.Compras(Compra_Fecha,Compra_Cantidad,Id_Usuario,Publicacion_Cod)
				VALUES (@Compra_Fecha,@Compra_Cantidad,@Id_Usr,@Publicacion_Cod)
				set @Publicacion_Cod  = (select max(ID_Compra) from GROUP_APROVED.Compras);    /*el id de publ pasa a ser id de compra a medios de minimizar y reutilizar variables*/
				set @Calificacion_Cant_Estrellas = @Calificacion_Cant_Estrellas/2;
				insert into GROUP_APROVED.Calificaciones(Calif_Cod, Calif_Cant_Est, Calif_Descr, ID_Compra)
				values(@Calificacion_Codigo, @Calificacion_Cant_Estrellas, @Calificacion_Descripcion, @Publicacion_Cod);



			end;
		else
			begin
				
				insert into GROUP_APROVED.Compras(Compra_Fecha,Compra_Cantidad,Id_Usuario,Publicacion_Cod)
				VALUES (@Compra_Fecha,@Compra_Cantidad,@Id_Usr,@Publicacion_Cod);

			end;
		fetch next from cursorCompCalif;
	end;
	close cursorCompCalif;
	deallocate cursorCompCalif;
end;

go


/*trigger para eliminar la relacion usuario-rol cunado se inhabilita un rol*/
create trigger GROUP_APROVED.quitarRol_Usuario
on GROUP_APROVED.Roles
after Update as 
begin
	declare @estado char;
	declare @id_Rol int;
	select @id_Rol=Id_Rol,@estado=estado from inserted;
	if(@estado='I')begin
	delete from RolesxUsuario where Id_Roles=@id_Rol;
	end
end
go
/*funcion para saber si un usuario ya califico una compra*/

create function GROUP_APROVED.usuarioYaCalifico(@idCompra numeric(18,0))
returns char(2) as begin
	declare @valor char(2)
	if exists(select 1 from GROUP_APROVED.Calificaciones where ID_Compra=@idCompra )begin
			set @valor='Si';
		end 
	else 
		begin
			set @valor= 'No';
		end
	return @valor
end;

go



CREATE procedure GROUP_APROVED.bajaLogicaUsuario

@idusuario int,
@Username nvarchar(255),
@respuesta int output

as
begin
	begin try
		update GROUP_APROVED.Usuarios
		set Estado = 'B'
		where Id_Usr = @idusuario and Username = @Username

		set @respuesta = 1
	end try
	begin catch
		set @respuesta = 0
	end catch
end

go

CREATE PROCEDURE GROUP_APROVED.CrearUsuarioCliente

/*Debe ingresar registro en tabla	-usuarios
									-clientes
									-rolesxusuario
									*/
--variables
@respuesta nvarchar(1000) output,
@Username nvarchar(255),
@Password nvarchar(255),
@Fecha_Creacion datetime,

@Dni_Cli numeric(18,0),
@Tipo_Dni nvarchar(7),
@Cli_Nombre  nvarchar(255),
@Cli_Apellido nvarchar(255),
@Cli_Fecha_Nac datetime,
@CLI_Telefono numeric(18,0),
@Cli_Mail nvarchar(255),
@Cli_Dom_Calle nvarchar(255),
@Cli_Nro_Calle numeric(18,0),
@Cli_Piso numeric(18,0),
@Cli_Depto nvarchar(50),
@Cli_Cod_Postal nvarchar(255)

AS
BEGIN
set @respuesta = ''

--primero inserta en usuarios
	begin try
		insert into GROUP_APROVED.Usuarios (Username,Passw,Fecha_Creacion,intentos)
		values (
		@Username, 
		convert(nvarchar(255),HASHBYTES('SHA2_256', @Password),1),
		convert(datetime,@Fecha_Creacion,103),
		0)
		set @respuesta = @respuesta + 'A'
	end try
	begin catch
		set @respuesta = @respuesta + 'B'
	end catch

--busco el id_usr que creó el insert anterior

declare @Id_Usr int
set @Id_Usr = (select Id_Usr from GROUP_APROVED.Usuarios where (Username = @Username))

--insercion en rolesxusuario
	begin try
		insert into GROUP_APROVED.RolesxUsuario(Id_Usr,Id_Roles)
		values (@Id_Usr, 2) --2 es el rol cliente
		set @respuesta = @respuesta + 'C'
	end try
	begin catch
		set @respuesta = @respuesta + 'D'
	end catch

--insercion en clientes
	begin try
		insert into GROUP_APROVED.Clientes 
		(Dni_Cli,
		Tipo_Dni,
		Cli_Nombre,
		Cli_Apellido,
		Cli_Fecha_Nac,
		CLI_Telefono,
		Cli_Mail,
		Cli_Dom_Calle,
		Cli_Nro_Calle,
		Cli_Piso,
		Cli_Depto,
		Cli_Cod_Postal,
		Id_Usuario)
		values (
				@Dni_Cli,
				@Tipo_Dni,
				@Cli_Nombre,
				@Cli_Apellido,
				@Cli_Fecha_Nac,
				@CLI_Telefono,
				@Cli_Mail,
				@Cli_Dom_Calle,
				@Cli_Nro_Calle,
				@Cli_Piso,
				@Cli_Depto,
				@Cli_Cod_Postal,
				@Id_Usr
				)
		set @respuesta = @respuesta + 'E'
	end try
	begin catch
		set @respuesta = @respuesta + 'F'
	end catch
	
END

go

CREATE PROCEDURE GROUP_APROVED.CrearUsuarioEmpresa

/*Debe ingresar registro en tabla	-usuarios
									-clientes
									-rolesxusuario
									*/
--variables
@respuesta nvarchar(255) output,
@Username nvarchar(255),
@Password nvarchar(255),
@Fecha_Creacion datetime,

@Empresa_Razon_Social nvarchar(100),
@Empresa_Cuit nvarchar(100),
@Empresa_Mail nvarchar(255),																			/*VERIFICAR FECHA CREACION EMPRESA, NO ES LO MISMO Q FECHA CREACION USUARIO (?)*/
@Empresa_Dom_Calle nvarchar (255),
@Empresa_Nro_Calle numeric(18,0),
@Empresa_Piso numeric(18,0),
@Empresa_Depto nvarchar(50),
@Empresa_Cod_Postal nvarchar(255),
@Empresa_Telefono numeric(18,0),
@Empresa_Nombre_Contacto nvarchar(255),
@Empresa_RubroP nvarchar(255)

AS
BEGIN
set @respuesta = ''

--primero inserta en usuarios
	begin try
		insert into GROUP_APROVED.Usuarios (Username,Passw,Fecha_Creacion,intentos)
		values (
		@Username, 
		convert(nvarchar(255),HASHBYTES('SHA2_256', @Password),1),
		convert(datetime,@Fecha_Creacion,103),
		0)
		set @respuesta = @respuesta + 'A'
	end try
	begin catch
		set @respuesta = @respuesta + 'B'
	end catch

--busco el id_usr que creó el insert anterior

declare @Id_Usr int
set @Id_Usr = (select Id_Usr from GROUP_APROVED.Usuarios where (Username = @Username))

--insercion en rolesxusuario
	begin try
		insert into GROUP_APROVED.RolesxUsuario(Id_Usr,Id_Roles)
		values (@Id_Usr, 3) --3 es el rol empresa
		set @respuesta = @respuesta + 'C'
	end try
	begin catch
		set @respuesta = @respuesta + 'D'
	end catch

--insercion en empresas
	begin try
		insert into GROUP_APROVED.Empresas
		(
		Empresa_Razon_Social,
		Empresa_Cuit,
		Empresa_Mail,
		Empresa_Dom_Calle,
		Empresa_Nro_Calle,
		Empresa_Piso,
		Empresa_Depto,
		Empresa_Fecha_Creacion,
		Empresa_Cod_Postal,
		Id_Usuario,
		Empresa_Telefono,
		Empresa_Nombre_Contacto,
		Empresa_RubroP
		)
		values (
		@Empresa_Razon_Social,
		@Empresa_Cuit,
		@Empresa_Mail,
		@Empresa_Dom_Calle,
		@Empresa_Nro_Calle,
		@Empresa_Piso,
		@Empresa_Depto,
		@Fecha_Creacion,
		@Empresa_Cod_Postal,
		@Id_Usr,
		@Empresa_Telefono,
		@Empresa_Nombre_Contacto,
		@Empresa_RubroP
				)
		set @respuesta = @respuesta + 'E'
	end try
	begin catch
		set @respuesta = @respuesta + 'F'
	end catch
	
END

go

CREATE procedure GROUP_APROVED.updateClientes

--todas las colummnas de los clientes...
@Dni_Cli numeric(18,0),
@Tipo_Dni nvarchar(7),
@Cli_Nombre  nvarchar(255),
@Cli_Apellido nvarchar(255),
@Cli_Fecha_Nac datetime,
@CLI_Telefono numeric(18,0),
@Cli_Mail nvarchar(255),
@Cli_Dom_Calle nvarchar(255),
@Cli_Nro_Calle numeric(18,0),
@Cli_Piso numeric(18,0),
@Cli_Depto nvarchar(50),
@Cli_Cod_Postal nvarchar(255),
@Id_Usr int,
@Estado nvarchar(255),
--la response
@respuesta nvarchar(255) output
as
begin
set @respuesta = ''
	begin try
	update GROUP_APROVED.Clientes
	set Dni_Cli=@Dni_Cli,
		Tipo_Dni=@Tipo_Dni,
		Cli_Nombre=@Cli_Nombre,
		Cli_Apellido=@Cli_Apellido,
		Cli_Fecha_Nac=convert(datetime,@Cli_Fecha_Nac,103),
		CLI_Telefono=@CLI_Telefono,
		Cli_Mail=@Cli_Mail,
		Cli_Dom_Calle=@Cli_Dom_Calle,
		Cli_Nro_Calle=@Cli_Nro_Calle,
		Cli_Piso=@Cli_Piso,
		Cli_Depto=@Cli_Depto,
		Cli_Cod_Postal=@Cli_Cod_Postal
		where Id_Usuario=@Id_Usr
	set @respuesta = @respuesta +'A'
	end try
	begin catch
	set @respuesta = @respuesta +'B'
	end catch

	begin try
		update GROUP_APROVED.Usuarios
		set Estado = @Estado
		where Id_Usr = @Id_Usr
		set @respuesta = @respuesta +'C'
	end try
	begin catch
		set @respuesta = @respuesta +'D'
	end catch
end


go

CREATE procedure GROUP_APROVED.updateEmpresa

--todas las colummnas de las empresas...
@Empresa_Razon_Social nvarchar(100),
	@Empresa_Cuit nvarchar(100),
	@Empresa_Mail nvarchar(255),
	@Empresa_Dom_Calle nvarchar (255),
	@Empresa_Nro_Calle numeric(18,0),
	@Empresa_Piso numeric(18,0),
	@Empresa_Depto nvarchar(50),
	@Empresa_Cod_Postal nvarchar(255),
	@Id_Usuario INT,
	@Empresa_Telefono numeric(18,0),
	@Empresa_Nombre_Contacto nvarchar(255),
	@Empresa_RubroP nvarchar(255),
@Estado nvarchar(255),
--la response
@respuesta nvarchar(255) output
as
begin
set @respuesta = ''
	begin try
	update GROUP_APROVED.Empresas
	set Empresa_Razon_Social =@Empresa_Razon_Social,
	Empresa_Cuit =@Empresa_Cuit,
	Empresa_Mail =@Empresa_Mail,
	Empresa_Dom_Calle =@Empresa_Dom_Calle,
	Empresa_Nro_Calle =@Empresa_Nro_Calle,
	Empresa_Piso =@Empresa_Piso,
	Empresa_Depto =@Empresa_Depto,
	Empresa_Cod_Postal =@Empresa_Cod_Postal,
	Empresa_Telefono =@Empresa_Telefono,
	Empresa_Nombre_Contacto =@Empresa_Nombre_Contacto,
	Empresa_RubroP =@Empresa_RubroP
	where Id_Usuario=@Id_Usuario
	set @respuesta = @respuesta +'A'
	end try
	begin catch
	set @respuesta = @respuesta +'B'
	end catch

	begin try
		update GROUP_APROVED.Usuarios
		set Estado = @Estado
		where Id_Usr = @Id_Usuario
		set @respuesta = @respuesta +'C'
	end try
	begin catch
		set @respuesta = @respuesta +'D'
	end catch
end;

go

/*drop procedure GROUP_APROVED.usrCreationCli
drop procedure GROUP_APROVED.usrCreationEmp
drop procedure GROUP_APROVED.funcionesAdmin
drop procedure GROUP_APROVED.funcionesCliente
drop procedure GROUP_APROVED.funcionesEmpresa
drop procedure GROUP_APROVED.migracionPubl
drop trigger GROUP_APROVED.quitarRol_Usuario
drop procedure GROUP_APROVED.bajaLogicaUsuario
drop procedure GROUP_APROVED.CrearUsuarioCliente
drop procedure GROUP_APROVED.CrearUsuarioEmpresa
drop procedure GROUP_APROVED.updateClientes
drop procedure GROUP_APROVED.updateEmpresa
drop procedure GROUP_APROVED.migrComprasCalif
*/


/*MIGRACION*/
	/*funciones*/

insert into GROUP_APROVED.Funciones(Desc_Func) values ('r')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('u')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('b')								
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('p')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('f')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('c')


	/*	r = 1 = abm rol			p = 4 = publicacion
		u = 2 = abm usuario		f = 5 = consultar facturas
		b = 3 = abm rubro		c = 6 = calificar*/



	/*roles*/
	
INSERT into GROUP_APROVED.Roles(Desc_Rol)
values('Administrador')

INSERT into GROUP_APROVED.Roles(Desc_Rol)
values('Cliente')

INSERT into GROUP_APROVED.Roles(Desc_Rol)
values('Empresa')



go
exec GROUP_APROVED.funcionesCliente;
go
exec GROUP_APROVED.funcionesAdmin;
go
exec GROUP_APROVED.funcionesEmpresa;
go

		/*clientes*/
INSERT into GROUP_APROVED.Clientes(Dni_Cli,Cli_Nombre, Cli_Apellido,  Cli_Fecha_Nac, Cli_Mail, Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal)
select distinct Cli_Dni, Cli_Nombre , Cli_Apeliido, Cli_Fecha_Nac, Cli_Mail, Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal from  gd_esquema.Maestra
WHERE cli_Dni is not null 



		/*empresas*/

insert into GROUP_APROVED.Empresas(Empresa_Razon_Social, Empresa_Cuit,Empresa_Fecha_Creacion,Empresa_Mail,Empresa_Dom_Calle,Empresa_Nro_Calle, Empresa_Piso, Empresa_Depto, Empresa_Cod_Postal)
select distinct Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Fecha_Creacion, Publ_Empresa_Mail, Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Piso, Publ_Empresa_Depto, Publ_Empresa_Cod_Postal from gd_esquema.Maestra
WHERE Publ_Empresa_Razon_Social is not null and Publ_Empresa_Cuit is not null


		/*usuarios*/
go
exec GROUP_APROVED.usrCreationCli;     /*select * from GROUP_APROVED.Usuarios*/

go


exec GROUP_APROVED.usrCreationEmp;

go

alter table GROUP_APROVED.Clientes
ADD CONSTRAINT ID_usrUniq UNIQUE(Id_Usuario);
go
alter table GROUP_APROVED.Empresas
ADD CONSTRAINT ID_usrUniqEMP UNIQUE(Id_Usuario);
go

		/*visibilidades*/
insert into GROUP_APROVED.Visibilidades(Visibilidad_Cod, Visibilidad_Desc, Visibilidad_Precio, Visibilidad_Porcentaje)
select distinct Publicacion_Visibilidad_Cod, Publicacion_Visibilidad_Desc, Publicacion_Visibilidad_Precio, Publicacion_Visibilidad_Porcentaje  from gd_esquema.Maestra order by 1


		/*rubros*/
insert into GROUP_APROVED.Rubros(Rubro_Desc_Completa)
select distinct Publicacion_Rubro_Descripcion from gd_esquema.Maestra


		/*Estados_publicaciones*/

insert into GROUP_APROVED.Estado_Publ(Descripcion)
values('Borrador')
insert into GROUP_APROVED.Estado_Publ(Descripcion)
values('Activa')
insert into GROUP_APROVED.Estado_Publ(Descripcion)
values('Pausada')
insert into GROUP_APROVED.Estado_Publ(Descripcion)
values('Finalizada')

		/*Publicaciones*/

exec GROUP_APROVED.migracionPubl
 
/* go
set identity_Insert GROUP_APROVED.Publicaciones off;    /*esto es para que al insertar nuevas publicaciones recuente normal sin tener que insertar pub_Cod*/
go*/


		/*compras y calificaciones */

exec GROUP_APROVED.migrComprasCalif

		/*ofertas*/
insert into GROUP_APROVED.Ofertas(Oferta_Fecha,Oferta_Monto,Id_Usuario,Publicacion_Cod)
select distinct m.Oferta_Fecha,m.Oferta_Monto,c.Id_Usuario,m.Publicacion_Cod from gd_esquema.Maestra m  join GROUP_APROVED.Clientes c on m.Cli_Dni = c.Dni_Cli where Oferta_Fecha is not null

	








/*----hice cambios aqui------------- */
declare @password nvarchar(255)---declarar estas variables fue la unica manera, para hacer funcionar el login
declare @username nvarchar(255)
set @username='Admin'
set @password='w23e'
insert into GROUP_APROVED.Usuarios(Username,Passw,intentos)
values(@username, convert(nvarchar(255),HASHBYTES('SHA2_256', @password),1),0)

--creo que deberia ser default 0 el numero de intentos en la tabla usuarios

insert into GROUP_APROVED.RolesxUsuario
values((select Id_Usr from GROUP_APROVED.Usuarios where Username = 'Admin'),(select Id_Rol from GROUP_APROVED.Roles where Desc_Rol = 'Administrador'))

