import HomePage from "./HomePage";
import {Dimensions} from "react-native";

import React from "react";


export default function ReactConversion(props) {

    const windowHeight = Dimensions.get("window").height;
    
        return (
          <HomePage height = {windowHeight} properties = {props}></HomePage>
        )
}