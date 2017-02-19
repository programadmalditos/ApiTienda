namespace ApiTienda.Model{
public class Producto{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Unidades { get; set; }
    public decimal Precio { get; set; }
    public virtual Tienda Tienda{get;set;}
    
}

}