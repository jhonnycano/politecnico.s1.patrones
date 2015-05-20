﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Politecnico.Patrones.ProyectoFinal.Lib.Entidades;
using Politecnico.Patrones.ProyectoFinal.Lib.VO;

namespace Politecnico.Patrones.ProyectoFinal.Lib.Tests.Aceptacion {
    [TestFixture]
    public class GestorDominioTest {
        private IGestorDominio _gestorDominio;
        private IGestorPersistencia _gestorPersistencia;
        private Mock<IGestorPersistencia> _mockGestorPersistencia;
        [SetUp]
        public void Inicializar() {
            _mockGestorPersistencia = new Mock<IGestorPersistencia>();
            _gestorPersistencia = _mockGestorPersistencia.Object;

            _gestorDominio = new GestorDominio(_gestorPersistencia);
        }

        [Test]
        public void AgregarCancion() {
            // Preparar
            _mockGestorPersistencia.Setup(gp => gp.Guardar(It.IsAny<Cancion>()));

            var entrada = new EditarCancionEntrada
                {
                    Nombre = "Cancion de pruebas",
                };

            // Invocar
            var salida = _gestorDominio.EditarCancion(entrada);

            // Revisar
            Assert.AreEqual(SalidaBase.Resultados.Exito, salida.Resultado);
            _mockGestorPersistencia.VerifyAll();
        }
        [Test]
        public void RelacionarInterpretesYCanciones_SiRelacionesNuevas_Exito() {
            // Preparar
            var relacionesCreadas = 0;
            var cancion = new Cancion {Id = 7, Nombre = "Al final de este viaje"};
            var interprete13 = new Interprete {Id = 13, Nombre = "Silvio Rodriguez"};
            var interprete17 = new Interprete {Id = 17, Nombre = "Pablo Milanes"};
            var interprete19 = new Interprete {Id = 19, Nombre = "Fernando Delgadillo"};

            _mockGestorPersistencia.Setup(gp => gp.TraerCancion(7)).Returns(cancion);
            _mockGestorPersistencia.Setup(gp => gp.TraerInterprete(13)).Returns(interprete13);
            _mockGestorPersistencia.Setup(gp => gp.TraerInterprete(17)).Returns(interprete17);
            _mockGestorPersistencia.Setup(gp => gp.TraerInterprete(19)).Returns(interprete19);

            _mockGestorPersistencia.Setup(gp => gp.Guardar(It.IsAny<CancionInterprete>()))
                .Callback(() => relacionesCreadas++);
            var entrada = new RelacionarInterpretesACancionEntrada
                {
                    CancionId = 7,
                    Interpretes = new List<int> {13, 17, 19},
                    Accion = RelacionarInterpretesACancionEntrada.Acciones.Agregar
                };

            // Invocar
            var salida = _gestorDominio.RelacionarInterpretesACancion(entrada);

            // Revisar
            Assert.AreEqual(SalidaBase.Resultados.Exito, salida.Resultado);
            Assert.AreEqual(3, relacionesCreadas);
            _mockGestorPersistencia.VerifyAll();
        }
    }
}