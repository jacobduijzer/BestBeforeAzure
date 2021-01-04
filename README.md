# BestBeforeAzure

As I have multiple members in my household with the celiac disease we have an extended stock of gluten free products. Because we often buy in bulk we have to be careful not to let products expire. The "Best Before" application is a website to manage products and stock, it will send (e-mail) about about to expire stock.

## System Context

The system contains two major components, the web application itself and an external system to send e-mail updates to the users. The diagram looks like this:

![System Context Diagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/jacobduijzer/BestBeforeAzure/main/documentation/assets/systemcontext.pu)

## Container Diagram

![Container Diagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/jacobduijzer/BestBeforeAzure/main/documentation/assets/containerdiagram.pu)

## Web Application - Component Diagram

![Component Diagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/jacobduijzer/BestBeforeAzure/main/documentation/assets/webapp-componentdiagram.pu)

## Stock Updates - Component Diagram

![Component Diagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/jacobduijzer/BestBeforeAzure/main/documentation/assets/stockupdates-componentdiagram.pu)