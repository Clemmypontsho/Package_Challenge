# Package_Challenge

The challenge is to create a cross-platform library to solve the package optimization problem. The goal was to develop a solution that can efficiently optimize packaging based on given constraints and produce the best combination of items for each package.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Project Structure](#project-structure)
- [Testing](#testing)

## Introduction

This project provides a library that optimizes packaging based on the given input file. It follows a set of rules and algorithms to select the most valuable combination of items for each package while considering weight limits. The library is designed to be easily integrable into different applications and platforms.

## Features

- Optimizes packaging based on item constraints and weight limits
- Provides a string representation of the optimized package list
- Handles exceptions for invalid input or missing files


## Project Structure

-The project is structured into three main projects:
-com.mobiquity.packer - Class library project that provides package optimization functionality.
-com.mobiquity - Console application acting as the presentation layer to demonstrate the library's usage.
-com.mobiquity.packer.tests - Unit test project containing tests for the package optimization library.

## Testing
-The com.mobiquity.packer.tests project contains unit tests for the package optimization library. 
-It follows a Test-Driven Development (TDD) approach to ensure the correctness of the implemented features.
-You can run the tests using your preferred unit testing framework

