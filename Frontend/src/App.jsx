import { useState } from 'react';
import Inicio from './components/inicio';
import Login from './components/login';
import Registro from './components/Registro';

function App() {
    const [vista, setVista] = useState('inicio');

    return (
        <div>
            {vista === 'inicio' && <Inicio cambiarVista={setVista} />}
            {vista === 'login' && <Login cambiarVista={setVista} />}
            {vista === 'registro' && <Registro cambiarVista={setVista} />}
        </div>
    );
}

export default App;