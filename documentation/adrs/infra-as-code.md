# Infra-As-Code with Terraform

## Status

Accepted

## Context

There are multiple ways to manage the Azure environment and resources:

- Manually
- Infra-As-Code
  - Azure ARM
  - Terraform
  - Anible
  - others 

While manually seems fast in the initial setup phase, it is more difficult to maintain. Infrastructure as code gives a documentated output of the infrastructure in declarative form. While there are many different solutions to choose from we are most experienced in Terraform.

## Decision

We will be using Terraform for the infra-as-code solution.
