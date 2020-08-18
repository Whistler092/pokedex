import * as React from 'react';
import { connect } from 'react-redux';
import Pokedex from './Pokedex';

const Home = () => (
  <div>
    <Pokedex />
    
  </div>
);

export default connect()(Home);
