using System.Linq;

namespace ApiTienda.Model{
public static class DbInitializer
{

    public static void Initialize(ListaCompraConext context)
    {
        if (context.Producto.Any())
        {
            return;
        }

        var tienda=new Tienda(){
            Nombre="Super del barrio",
            Direccion="Al lado de casa"
        };

        context.Tienda.Add(tienda);

        var productos = new Producto[]{
    new Producto(){
        
        Nombre="Leche",
        Descripcion="Para el cafe",
        Unidades=1,
        Precio=0.7M,
        Tienda=tienda
        
    },
new Producto(){
        
        Nombre="Jamon",
        Descripcion="Para el desayuno",
        Unidades=3,
        Precio=5M,
        Tienda=tienda
    },
};
        foreach (var compra in productos)
        {
            context.Producto.Add(compra);
        }
        context.SaveChanges();

    }
}

}