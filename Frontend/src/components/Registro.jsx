import { useState } from 'react';

function Registro() {
    const [nombre, setNombre] = useState('');
    const [apellido, setApellido] = useState('');
    const [correo, setCorreo] = useState('');
    const [alias, setAlias] = useState('');
    const [contrasena, setContrasena] = useState('');

    const [errores, setErrores] = useState({});

    function validar() {
        const nuevosErrores = {};

        if (!nombre.trim()) {
            nuevosErrores.nombre = 'El nombre es obligatorio.';
        }

        if (!apellido.trim()) {
            nuevosErrores.apellido = 'El apellido es obligatorio.';
        }

        if (!correo.trim()) {
            nuevosErrores.correo = 'El correo es obligatorio.';
        } else if (!correo.includes('@')) {
            nuevosErrores.correo = 'Ingresa un correo válido.';
        }

        if (!alias.trim()) {
            nuevosErrores.alias = 'El alias es obligatorio.';
        }

        if (!contrasena) {
            nuevosErrores.contrasena = 'La contraseña es obligatoria.';
        } else if (contrasena.length < 4) {
            nuevosErrores.contrasena = 'La contraseña debe tener al menos 4 caracteres.';
        }

        return nuevosErrores;
    }

    async function manejarRegistro(evento) {
        evento.preventDefault();

        const erroresEncontrados = validar();

        if (Object.keys(erroresEncontrados).length > 0) {
            setErrores(erroresEncontrados);
            return;
        }

        setErrores({});

        try {
            const respuesta = await fetch('https://localhost:7025/api/usuarios/registro', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    idUsu: 'U' + Math.floor(Math.random() * 1000),
                    nomUsu: nombre,
                    apeUsu: apellido,
                    corUsu: correo,
                    aliasUsu: alias,
                    conUsu: contrasena,
                    esPriv: false,
                    fecReg: new Date().toISOString().split('T')[0]
                })
            });

            if (respuesta.ok) {
                alert('¡Usuario registrado!');
                setNombre('');
                setApellido('');
                setCorreo('');
                setAlias('');
                setContrasena('');
            } else {
                const datos = await respuesta.json();
                setErrores({ servidor: 'No se pudo registrar. Revisa los datos e intenta de nuevo.' });
                console.error('Detalle del error:', datos);
            }
        } catch (error) {
            console.error('Error de conexión:', error);
            setErrores({ servidor: 'No se pudo conectar con el servidor.' });
        }
        
    }

    return (
        <form onSubmit={manejarRegistro}>
            <h2>Registro</h2>

            <div>
                <input
                    type="text"
                    placeholder="Nombre"
                    value={nombre}
                    onChange={(e) => setNombre(e.target.value)}
                />
                {errores.nombre && <p style={{ color: 'red', margin: '2px 0' }}>{errores.nombre}</p>}
            </div>

            <div>
                <input
                    type="text"
                    placeholder="Apellido"
                    value={apellido}
                    onChange={(e) => setApellido(e.target.value)}
                />
                {errores.apellido && <p style={{ color: 'red', margin: '2px 0' }}>{errores.apellido}</p>}
            </div>

            <div>
                <input
                    type="email"
                    placeholder="Correo"
                    value={correo}
                    onChange={(e) => setCorreo(e.target.value)}
                />
                {errores.correo && <p style={{ color: 'red', margin: '2px 0' }}>{errores.correo}</p>}
            </div>

            <div>
                <input
                    type="text"
                    placeholder="Alias"
                    value={alias}
                    onChange={(e) => setAlias(e.target.value)}
                />
                {errores.alias && <p style={{ color: 'red', margin: '2px 0' }}>{errores.alias}</p>}
            </div>

            <div>
                <input
                    type="password"
                    placeholder="Contraseña"
                    value={contrasena}
                    onChange={(e) => setContrasena(e.target.value)}
                />
                {errores.contrasena && <p style={{ color: 'red', margin: '2px 0' }}>{errores.contrasena}</p>}
            </div>

            {errores.servidor && <p style={{ color: 'red', fontWeight: 'bold' }}>{errores.servidor}</p>}

            <button type="submit">Registrarme</button>
        </form>
    );
}


export default Registro;