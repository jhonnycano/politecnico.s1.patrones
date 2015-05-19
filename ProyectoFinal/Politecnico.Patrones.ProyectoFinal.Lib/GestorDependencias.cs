﻿namespace Politecnico.Patrones.ProyectoFinal.Lib {
    public class GestorDependencias {
        private readonly GestorPersistenciaEF _gestorPersistencia;
        public GestorDependencias() {
            _gestorPersistencia = new GestorPersistenciaEF();
        }
        public IGestorAutenticacion TraerGestorAutenticacion() {
            return new GestorAutenticacion(_gestorPersistencia);
        }
        public IGestorDominio TraerGestorDominio() {
            return new GestorDominio(_gestorPersistencia);
        }
    }
}