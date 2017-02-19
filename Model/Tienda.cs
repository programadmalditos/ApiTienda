using System.Collections.Generic;

namespace ApiTienda.Model{

public class Tienda{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }

    public virtual ICollection<Producto> Compras { get; set; }
}

}