use GD1C2016

/* 

creacion de schema*/

go

CREATE SCHEMA [GROUP_APROVED] AUTHORIZATION [gd]


go

/*CREACION DE TABLAS*/
begin transaction createTables
CREATE TABLE GROUP_APROVED.Funciones (
	Id_Func INT PRIMARY KEY IDENTITY(1,1),
	Desc_Func nvarchar,

)

CREATE TABLE GROUP_APROVED.Roles (
	Id_Rol int IDENTITY(1,1) PRIMARY KEY,
	Desc_Rol nvarchar(255)	
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



SET IDENTITY_INSERT GROUP_APROVED.Publicaciones ON;

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
	ID_Compra numeric(18,0) REFERENCES GROUP_APROVED.Compras,
	Id_Usuario INT REFERENCES GROUP_APROVED.Usuarios,

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


	Create Procedure usrCreationCli

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

	CREATE Procedure usrCreationEmp

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
			set @usr = concat(substring(@RazonS,17,2),substring(@Cuit,4,5)) 	;     /*creacion de usuario a partir de primera letra del nombre y primeras 3 letras del ape ape (?)*/

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



CREATE PROCEDURE funcionesAdmin

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

CREATE PROCEDURE funcionesCliente

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

CREATE PROCEDURE funcionesEmpresa

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
	
/*MIGRACION*/
	/*funciones*/

insert into GROUP_APROVED.Funciones(Desc_Func) values ('r')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('u')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('b')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('p')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('f')
insert into GROUP_APROVED.Funciones(Desc_Func)  values ('c')



	/*roles*/
	
INSERT into GROUP_APROVED.Roles(Desc_Rol)
values('Administrador')

INSERT into GROUP_APROVED.Roles(Desc_Rol)
values('Cliente')

INSERT into GROUP_APROVED.Roles(Desc_Rol)
values('Empresa')



go
exec funcionesCliente;
go
exec funcionesAdmin;
go
exec funcionesEmpresa;
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
exec usrCreationCli;     /*select * from GROUP_APROVED.Usuarios*/

go


exec usrCreationEmp;

go
		/*visibilidades*/
insert into GROUP_APROVED.Visibilidades(Visibilidad_Cod, Visibilidad_Desc, Visibilidad_Precio, Visibilidad_Porcentaje)
select distinct Publicacion_Visibilidad_Cod, Publicacion_Visibilidad_Desc, Publicacion_Visibilidad_Precio, Publicacion_Visibilidad_Porcentaje  from gd_esquema.Maestra order by 1


		/*rubros*/
insert into GROUP_APROVED.Rubros(Rubro_Desc_Completa)
select distinct Publicacion_Rubro_Descripcion from gd_esquema.Maestra



insert into GROUP_APROVED.Publicaciones(Publicacion_Cod, Publicacion_Desc, Publicacion_Fecha, Publicacion_Fecha_Venc, Publicacion_Precio, Publicacion_Stock)

select distinct Publicacion_Cod, Publicacion_Descripcion, Publicacion_Fecha, Publicacion_Fecha_Venc, Publicacion_Precio, Publicacion_Stock from gd_esquema.Maestra order by 1
 
 go
set identity_Insert GROUP_APROVED.Publicaciones off;
go
