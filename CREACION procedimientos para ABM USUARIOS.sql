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
end
