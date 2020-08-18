import React, { PureComponent } from 'react'
import * as PokedexStore from '../store/actions/PokedexActions';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { Pagination, PaginationItem, PaginationLink } from 'reactstrap';

type PokedexProps =
    PokedexStore.PokedexState
    & typeof PokedexStore.actionCreators
    & RouteComponentProps<{ index: string }>;

class Pokedex extends PureComponent<PokedexProps> {

    constructor(props: Readonly<PokedexProps>) {
        super(props)
        this.state = {
            searchText: ''
        }

        this.onChangeSearchText = this.onChangeSearchText.bind(this)
        this.prevPage = this.prevPage.bind(this);
        this.nextPage = this.nextPage.bind(this);
    }


    componentDidMount() {
        //const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        this.props.requestInitialPokemon(0);
    }

    prevPage() {
        console.log('prevPage', this.props.previous)
        if (this.props.previous) {
            const page = this.props.previous.substring(41, 55).split('&')[0];
            this.props.requestInitialPokemon(parseInt(page));
        }else {
            this.props.requestInitialPokemon(0);
        }
    }
    nextPage() {
        console.log('nextPage', this.props.next)
        if (this.props.next) {
            const page = this.props.next.substring(41, 55).split('&')[0];
            this.props.requestInitialPokemon(parseInt(page));
        }
    }

    private renderPagination() {
        //console.log('renderPagination', this.props)
        let totalPages = parseInt(this.props.count / 50);
        if (totalPages === 0) {
            return <p>Loading...</p>;
        }
        if (totalPages > 5) {
            totalPages = 5;
        }

        return (
            <Pagination aria-label="Page navigation example">
                <PaginationItem>
                    <PaginationLink previous href="#" onClick={this.prevPage} />
                </PaginationItem>
                <PaginationItem>
                    <PaginationLink next href="#" onClick={this.nextPage} />
                </PaginationItem>
            </Pagination>
        )       
    }

    private renderPokemons(pokemons: any[]) {
        //console.log('renderPokemons', pokemons);
        return (
            <React.Fragment >
                {pokemons.map(pokemon => (
                    <div className="col-sm-3" key={pokemon.name}>
                        <div className="pokemon-container-title">
                            <p className="pokemon-container-title-p">
                                <a className="pokemon-container-title-a"
                                    href={`/pokemon-details/${pokemon.id}`}><strong>{pokemon.name}</strong> </a>
                            </p>
                        </div>
                        <a href={`/pokemon-details/${pokemon.id}`}>
                            <img className="card-img-top"
                                src={pokemon.photo} alt="Card image cap" />
                        </a>
                        <div className="pokemon-container-description">
                            {pokemon.types && pokemon.types.map((type: string) => (
                                <p className="pokemon-container-description-line" key={type}>
                                    <i className="fas fa-film"></i> {type}
                                </p>
                            ))}

                        </div>
                    </div>
                ))}
            </React.Fragment>

        )
    }

    onChangeSearchText(e: { target: { value: any; }; }) {
        this.setState({
            searchText: e.target.value
        })
    }

    handleKeyDown = (e: { key: string; }) => {
        console.log("handleKeyDown", e, this.state)
        if (e.key === 'Enter') {
            /*  this.props.addTodo(this.state.todotext); */
            this.props.searchPokemon(this.state.searchText);
            this.setState({ searchText: '' })
        }
    }

    render() {
        //console.log('Pokedex Props', this.props)
        return (
            <div className="container">

                <h1 className="poke-header">Popular Pokemons</h1>
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="basic-addon3">Search for a Pokemon...</span>
                    </div>
                    <input type="text"
                        className="form-control"
                        id="searchPokemon"
                        aria-describedby="basic-addon3"
                        onChange={this.onChangeSearchText}
                        value={this.state.searchText}
                        onKeyDown={this.handleKeyDown}
                    /> 
                     <button type="button" 
                            onClick={() => {
                                this.props.searchPokemon(this.state.searchText);
                                this.setState({ searchText: '' })
                            } }
                            className="btn btn-success" >Search</button>
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

