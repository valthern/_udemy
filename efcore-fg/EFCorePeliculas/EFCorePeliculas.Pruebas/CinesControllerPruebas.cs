using EFCorePeliculas.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
    [TestClass]
    public class CinesControllerPruebas : BasePruebas
    {
        [TestMethod]
        public async Task Get_MandoLatitudYLongitudDeSD_Obtengo2CinesCercanos()
        {
            var longitud = 18.481139;
            var latitud = -69.938950;

            using (var context = LocalDBInicializador.GetDbContextLocalDb())
            {
                // Arrange
                var mapper = ConfigurarAutoMapper();
                var controller = new CinesController(context, mapper, actualizadorObservableCollection: null);

                // Act
                var respuesta = await controller.Get(latitud, longitud);
                var objectResult = respuesta as ObjectResult;
                var cines = (IEnumerable<object>)objectResult.Value;

                // Assert
                Assert.AreEqual(2, cines.Count());
            }
        }
    }
}
