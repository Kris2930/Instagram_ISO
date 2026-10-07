//import './Inicio.css';

function Inicio({ cambiarVista }) {
    return (
        <div className="pantalla-inicio">
            <div className="caja-inicio">
                <h1 className="logo-inicio">InstaClon</h1>
                <p className="eslogan-inicio">Comparte tus momentos con tu comunidad universitaria</p>

                <button className="boton-primario-inicio" onClick={() => cambiarVista('login')}>
                    Iniciar sesión
                </button>

                <div className="separador-inicio">
                    <span>¿No tienes cuenta?</span>
                </div>

                <button className="boton-secundario-inicio" onClick={() => cambiarVista('registro')}>
                    Crear cuenta nueva
                </button>
            </div>
        </div>
    );
}

export default Inicio;