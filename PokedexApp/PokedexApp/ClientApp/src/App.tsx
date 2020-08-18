import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';

import './custom.css'
import PokedexDetails from './components/PokemonDetail';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/pokemon-details/:id?' component={PokedexDetails} />
    </Layout>
);
