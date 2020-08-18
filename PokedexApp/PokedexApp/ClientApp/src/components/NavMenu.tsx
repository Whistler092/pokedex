import * as React from 'react';
import {
    Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink,
    Button, Form, FormGroup, Label, Input
} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


export default class NavMenu extends React.PureComponent<{}, { isOpen: boolean }> {
    public state = {
        isOpen: false
    };

    private registerForm() {
        return (
            <Form inline>
                <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                    <Label for="textFullName" className="mr-sm-2">Name</Label>
                    <Input type="text" name="FullName" id="textFullName" placeholder="ash ketchum" />
                </FormGroup>
                <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                    <Label for="textEmail" className="mr-sm-2">Email</Label>
                    <Input type="email" name="email" id="textEmail" placeholder="ash@poke.cool" />
                </FormGroup>
                <Button>Ingresar</Button>
            </Form>
        );
    }

    public render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">PokedexApp</NavbarBrand>
                        <NavbarToggler onClick={this.toggle} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
                            <ul className="navbar-nav flex-grow">
                                {this.registerForm()}
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }

    private toggle = () => {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }
}
