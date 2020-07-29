import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>World Doom League</h1>
    <p>This site is a prototype. Please navigate using the navbar at the top.</p>
  </div>
);

export default connect()(Home);
