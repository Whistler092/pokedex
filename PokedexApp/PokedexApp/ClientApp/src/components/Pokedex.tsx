import React, { PureComponent } from 'react'
import * as PokedexStore from '../store/actions/PokedexActions';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';

type PokedexProps =
    PokedexStore.PokedexState
    & typeof PokedexStore.actionCreators
    & RouteComponentProps<{ index: string }>;

class Pokedex extends PureComponent<PokedexProps> {

    componentDidMount() {
        //const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        this.props.requestInitialPokemon(0);
    }

    private renderPagination() {
        return (
            <nav aria-label="Page navigation">
                <ul className="pagination justify-content-center">
                    <li className="page-item">
                        <a className="page-link" aria-label="Previous" > {/* onClick="last_page()" */}
                            <span aria-hidden="true">&laquo;</span>
                            <span className="sr-only">Previous</span>
                        </a>
                    </li>
                    <li className="page-item" > {/* *ngFor="let pag of pages" */}
                        <a className="page-link" >1</a> {/* /*  pag  */}
                    </li>
                    <li className="page-item">
                        <a className="page-link" aria-label="Next" >{/* (click)="next_page()" */}
                            <span aria-hidden="true">&raquo;</span>
                            <span className="sr-only">Next</span>
                        </a>
                    </li >
                </ul >
            </nav >
        )
    }

    private renderPokemons(pokemons: any[]) {
        console.log('renderPokemons', pokemons);
        return (
            <React.Fragment >
                 {pokemons.map(pokemon => (
                     <div className="col-sm-3" key={pokemon.name}>
                        <a href="/pokemon/id">
                            <img className="card-img-top"
                             src={pokemon.photo} alt="Card image cap" />
                        </a>
                        <div className="pokemon-container-description">
                            <p>
                                <a href="/pokemon/id"><strong>{pokemon.name}</strong> </a>
                            </p>
                            <p className="pokemon-container-description-line">
                                <i className="fas fa-film"></i> {pokemon.types}
                        </p>
                        </div>
                    </div>
                ))}
            </React.Fragment>
               
        )
    }

    render() {
        console.log('Pokedex Props', this.props)
        return (
            <div className="container">

                <h1 className="movie-header">Popular Pokemons</h1>
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="basic-addon3">Search for a Pokemon...</span>
                    </div>
                    <input type="text" className="form-control" id="basic-url" aria-describedby="basic-addon3" /> {/* (keyup)="searchMovie($event)" */}
                </div>
                <div className="row">
                    {this.renderPokemons(this.props.pokemons)}
                </div>
                <div className="pokemon-container-paginate">
                    {this.renderPagination()}
                </div >
            </div >
        )
    }
}



export default connect(
    (state: ApplicationState) => state.pokedex, // Selects which state properties are merged into the component's props
    PokedexStore.actionCreators // Selects which action creators are merged into the component's props
)(Pokedex as any);

