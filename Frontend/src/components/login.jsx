import { useState } from 'react';
//import './Login.css';

function Login({ cambiarVista }) {
    const [correo, setCorreo] = useState('');
    const [contrasena, setContrasena] = useState('');
    const [errores, setErrores] = useState({});

    function validar() {
        const nuevosErrores = {};
        if (!correo.trim()) {
            nuevosErrores.correo = 'El correo o alias es obligatorio.';
        }
        if (!contrasena) {
            nuevosErrores.contrasena = 'La contraseña es obligatoria.';
        }
        return nuevosErrores;
    }

    async function manejarLogin(evento) {
        evento.preventDefault();
        const erroresEncontrados = validar();

        if (Object.keys(erroresEncontrados).length > 0) {
            setErrores(erroresEncontrados);
            return;
        }

        setErrores({});

        try {
            const respuesta = await fetch('https://localhost:7025/api/usuarios/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    corUsu: correo,
                    conUsu: contrasena
                })
            });

            if (respuesta.ok) {
                const datos = await respuesta.json();
                alert(`¡Bienvenido, ${datos.nomUsu}!`);
                // Más adelante aquí navegamos al feed principal
            } else {
                const datos = await respuesta.json();
                setErrores({ servidor: datos.mensaje || 'No se pudo iniciar sesión.' });
            }
        } catch (error) {
            console.error('Error de conexión:', error);
            setErrores({ servidor: 'No se pudo conectar con el servidor.' });
        }
    }

    return (
        <div className="pantalla-login">
            <form className="caja-login" onSubmit={manejarLogin}>
                <h1 className="logo-login">InstaClon</h1>

                <input
                    type="text"
                    placeholder="Correo o alias"
                    value={correo}
                    onChange={(e) => setCorreo(e.target.value)}
                />
                {errores.correo && <p className="mensaje-error-login">{errores.correo}</p>}

                <input
                    type="password"
                    placeholder="Contraseña"
                    value={contrasena}
                    onChange={(e) => setContrasena(e.target.value)}
                />
                {errores.contrasena && <p className="mensaje-error-login">{errores.contrasena}</p>}

                {errores.servidor && <p className="mensaje-error-login">{errores.servidor}</p>}

                <button type="submit" className="boton-primario-login">Iniciar sesión</button>

                <p className="volver-login" onClick={() => cambiarVista('inicio')}>
                    ← Volver al inicio
                </p>
            </form>

        </div>
    );
}

export default Login;