# Web application vs Mobile Application

## Status

Accepted

## Context

There are many ways to create an application so users can have a good user experience. For this application there are two valid options:

1. A web application
2. A mobile application (Xamarin Forms)

### Web application

Pro's

- Easily accessible from any device
- No synchronisation needed for multiple users

Con's

- More complex UI (HTML / javascript)
- No push notifications(?)

### Mobile Application

Pro's

- Many people have there phones always available
- Easy scanning of products (barcodes)
- Push notifications

Con's

- More dificult technology 
- If created for multiple users there needs to be a synchronisation mechanism

## Decision

We will use a **web application**. It is just easier and faster to release and maintain.

## Consequences

There will be a need for some kind of email system to send updates about expiring products.

