import React, { PureComponent } from 'react'
import * as PokedexStore from '../store/actions/PokedexActions';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { Jumbotron, Media } from 'reactstrap';

type PokedexProps =
    PokedexStore.PokedexState
    & typeof PokedexStore.actionCreators
    & RouteComponentProps<{ id: string }>;

class PokemonDetail extends PureComponent<PokedexProps> {

    componentDidMount() {
        const pokemonId = parseInt(this.props.match.params.id, 10) || 0;
        //const pokemonId = 101;
        //console.log('PokemonDetail', pokemonId);
        this.props.requestPokemonDetail(pokemonId);
    }

    render() {
        //console.log('PokemonDetail Props', this.props)
        if (this.props.pokemonDetail === undefined)
            return null;
        const poke = this.props.pokemonDetail;

        return (
            <div>
                <Jumbotron>
                    <h1 className="display-3">{`# ${poke.id} ${poke.name}`}</h1>
                    <p className="lead">{`height: ${poke.height}`}</p>
                    <p className="lead">{`weight: ${poke.weight}`}</p>
                    <hr className="my-2" />
                    <Media>
                        <Media left href="#">
                            <Media object src={poke.photo} alt="Generic placeholder image" />
                        </Media>
                        <Media body>
                            <Media heading>
                                Tipo / Tipos
                            </Media>
                            {poke.types && poke.types.map((type: string) => (
                                <p className="pokemon-container-description-line" key={type}>
                                    <i className="fas fa-film"></i> {type}
                                </p>
                            ))}
                        </Media>
                    </Media>
                    <hr className="my-2" />
                    <p className="lead">Evolution Chain: 
                        {poke.evolutionChain.map(i => (
                            <li key={i}>{i}</li>
                        ))}
                    </p>
                    <hr className="my-2" />
                    <p className="lead">Moves:
                        {poke.moves.map(i => (
                        <li key={i}>{i}</li>
                    ))}

                    </p>
                </Jumbotron>
            </div>
        )
    }
}



export default connect(
    (state: ApplicationState) => state.pokedex, // Selects which state properties are merged into the component's props
    PokedexStore.actionCreators // Selects which action creators are merged into the component's props
)(PokemonDetail as any);

