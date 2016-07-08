 
 --vendedores con mas productos no vendidos
 
 select TOP 5 COUNT(P.Publicacion_Stock), U.Username, c.Cli_Nombre, c.Cli_Apellido from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on c.Id_Usuario = u.Id_Usr join GROUP_APROVED.Publicaciones p ON U.Id_Usr= P.Id_Usuario left join GROUP_APROVED.Visibilidades V ON P.Visibilidad_Cod = v.Visibilidad_Cod
 WHERE 
 year(p.Publicacion_Fecha) = @añobusacod and month(p.Publicacion_Fecha) between @mesinit and @mesfin and v.Visibilidad_Porcentaje between 0.15 and 0.30
 GROUP BY U.Username,c.Cli_Nombre, c.Cli_Apellido
 order by 1 DESC,  avg (v.Visibilidad_Porcentaje)
 

-- compradores con ams productos comprados

 select TOP 5 u.Username, count(Compra_Cantidad), c1.Cli_Apellido, c1.Cli_Nombre from GROUP_APROVED.Clientes c1 join GROUP_APROVED.Usuarios u on c1.Id_Usuario = u.Id_Usr join GROUP_APROVED.Compras c on u.Id_Usr = c.Id_Usuario 
 left join GROUP_APROVED.Publicaciones p on c.Publicacion_Cod = p.Publicacion_Cod join GROUP_APROVED.Rubros r on p.Id_Rubro = r.Id_Rubro
 where 
 year(c.Compra_Fecha) =@añobusacod and month(c.Compra_Fecha) between @mesinit and @messfin
 and r.Rubro_Desc_Corta = @rubroelegido
 group by u.Username, c1.Cli_Apellido, c1.Cli_Nombre
 order by 1 DESC 

-- vendedores con mayora cantidad de facutras

select TOP 5 u.Username, c.Cli_Apellido, c.Cli_Nombre, count(f.Nro_Fact) from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on c.Id_Usuario = u.Id_Usr join GROUP_APROVED.Publicaciones p ON U.Id_Usr= P.Id_Usuario join GROUP_APROVED.Facturas f on p.Publicacion_Cod = f.Publicacion_Cod
where year(f.Fact_Fecha) = @añobusacod and month(f.Fact_Fecha) = @mesbuscado
group by u.Username, c.Cli_Apellido, c.Cli_Nombre
order by 1 DESC


--vendedores con mayor monto facurado

select TOP 5 u.Username, c.Cli_Apellido, c.Cli_Nombre, SUM(f.Fact_Total) from GROUP_APROVED.Clientes c join GROUP_APROVED.Usuarios u on c.Id_Usuario = u.Id_Usr join GROUP_APROVED.Publicaciones p ON U.Id_Usr= P.Id_Usuario join GROUP_APROVED.Facturas f on p.Publicacion_Cod = f.Publicacion_Cod
where year(f.Fact_Fecha) = @añobusacod and month(f.Fact_Fecha) = @mesbuscado
group by u.Username, c.Cli_Apellido, c.Cli_Nombre
order by 1 DESC
