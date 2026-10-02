using EFCorePeliculas.Pruebas.Mocks;
using EFCorePeliculas.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCorePeliculas.Pruebas
{
    [TestClass]
    public class ActualizadorObservableCollectionPruebas
    {
        [TestMethod]
        public void Actualizar_SiEntidadesEsVacio_EntoncesTodosLosDTOsPasanAFormarParteDeEntidades()
        {
            // Arrange
            var mapeador = new Mapeador();
            var actualizadorObervableCollection = new ActualizadorObservableCollection(mapeador);
            var entidades = new ObservableCollection<ConId>();
            var dtos = new List<ConId>() { new ConId { Id = 1 }, new ConId { Id = 2 } };

            // Act
            actualizadorObervableCollection.Actualizar(entidades, dtos);

            // Assert
            Assert.AreEqual(2, entidades.Count);
            Assert.AreEqual(1, entidades[0].Id);
            Assert.AreEqual(2, entidades[1].Id);
        }

        [TestMethod]
        public void Actualizar_SiDTOsEsVacio_EntoncesTodasLasEntidadesSonRemovidas()
        {
            //Arrange
            var mapeador = new Mapeador();
            var actualizadorObservableCollection = new ActualizadorObservableCollection(mapeador);
            var entidades = new ObservableCollection<ConId>() { new ConId { Id = 1 }, new ConId { Id = 2 } };
            var dtos = new List<ConId>();

            // Act
            actualizadorObservableCollection.Actualizar(entidades, dtos);

            // Assert
            Assert.AreEqual(0, entidades.Count);
        }

        [TestMethod]
        public void Actualizar_SiDTOsYEntidadesTienenLosMismosObjetos_EntoncesLasCantidadesDeLasColeccionesNoSeAlteran()
        {
            // Arrange
            var mapeador=new Mapeador();
            var actualizadorObervableCollection = new ActualizadorObservableCollection(mapeador);
            var entidades = new ObservableCollection<ConId>() { new ConId { Id = 1 }, new ConId { Id = 2 } };
            var dtos = new List<ConId>() { new ConId { Id = 1 }, new ConId { Id = 2 } };

            // Act
            actualizadorObervableCollection.Actualizar(entidades , dtos);

            // Assert
            Assert.AreEqual(2, entidades .Count);
            Assert.AreEqual(2, dtos .Count);
        }
    }
}
