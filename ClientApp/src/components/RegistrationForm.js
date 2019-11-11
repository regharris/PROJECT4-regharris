import React from "react";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
// import { USERS_API_productImage } from "../Constants/index";
import { USERS_API_URL } from "../Constants";
class RegistrationForm extends React.Component {
  state = {
    id: 0,
    productName: "",
    productCategory: "",
    productDescription: "",
    productImage: ""
  };
  componentDidMount() {
    if (this.props.user) {
      const {
        id,
        productName,
        productCategory,
        productDescription,
        productImage
      } = this.props.user;
      this.setState({
        id,
        productName,
        productCategory,
        productDescription,
        productImage
      });
    }
  }
  onChange = e => {
    this.setState({ [e.target.name]: e.target.value });
  };
  submitNew = e => {
    e.preventDefault();
    fetch(`${USERS_API_URL}`, {
      method: "post",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        productName: this.state.productName,
        productDescription: this.state.productDescription,
        productImage: this.state.productImage,
        productCategory: this.state.productCategory
      })
    })
      .then(res => res.json())
      .then(user => {
        this.props.addUserToState(user);
        this.props.toggle();
      })
      .catch(err => console.log(err));
  };
  submitEdit = e => {
    e.preventDefault();
    fetch(`${USERS_API_URL}/${this.state.id}`, {
      method: "put",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        id: this.state.id,
        productName: this.state.productName,
        productDescription: this.state.productDescription,
        productImage: this.state.productImage,
        productCategory: this.state.productCategory
      })
    })
      .then(() => {
        this.props.toggle();
        this.props.updateUserIntoState(this.state.id);
      })
      .catch(err => console.log(err));
  };
  render() {
    return (
      <Form onSubmit={this.props.user ? this.submitEdit : this.submitNew}>
        <FormGroup>
          <Label for="productName">Product:</Label>
          <Input
            type="text"
            name="productName"
            onChange={this.onChange}
            value={this.state.productName === "" ? "" : this.state.productName}
          />
        </FormGroup>
        <FormGroup>
          <Label for="productCategory">Category</Label>
          <Input
            type="text"
            name="productCategory"
            onChange={this.onChange}
            value={
              this.state.productCategory === null
                ? ""
                : this.state.productCategory
            }
          />
        </FormGroup>
        <FormGroup>
          <Label for="category">Description</Label>
          <Input
            type="text"
            name="category"
            onChange={this.onChange}
            value={
              this.state.productDescription === null
                ? ""
                : this.state.productDescription
            }
          />
        </FormGroup>
        <FormGroup>
          <Label for="productImage">Picture</Label>
          <Input
            type="text"
            name="productImage"
            onChange={this.onChange}
            value={
              this.state.productImage === null ? "" : this.state.productImage
            }
            placeholder="./image/"
          />
        </FormGroup>
        <Button>Send</Button>
      </Form>
    );
  }
}
export default RegistrationForm;
