import React, { Component } from "react";
import styles from "./heading.module.scss";

export class Heading extends Component {
  render() {
    // reference as a js object
    return <h1 className={styles.error}>Some error header</h1>;
  }
}
