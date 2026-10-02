using EFCorePeliculas.Servicios;

namespace EFCorePeliculas.Pruebas
{
    [TestClass]
    public class ServicioUsuarioPruebas
    {
        [TestMethod]
        public void ObtenerUsuarioId_NoTraeValorVacioONulo()
        {
            // Arrange (Preparación)
            var servicioUsuario = new ServicioUsuario();

            // Act (Prueba)
            var resultado = servicioUsuario.ObtenerUsuarioId();

            // Assert (Verificación)
            Assert.IsNotNull(resultado);
            Assert.AreNotEqual("", resultado);
        }
    }
}
