# Budgerigar 
[![Build status](https://ci.appveyor.com/api/projects/status/github/visualeyes/budgerigar?branch=master&svg=true)](https://ci.appveyor.com/project/visualeyes-builder/budgerigar/branch/master) 
[![Coverage Status](https://coveralls.io/repos/visualeyes/budgerigar/badge.svg?branch=master&service=github)](https://coveralls.io/github/visualeyes/budgerigar?branch=master)
[![Budgerigar Nuget Version](https://img.shields.io/nuget/v/budgerigar.svg)](https://www.nuget.org/packages/Budgerigar/)
[![Budgerigar Nuget Downloads](https://img.shields.io/nuget/dt/budgerigar.svg)](https://www.nuget.org/packages/Budgerigar/)

## What is Budgerigar?
Budgerigar is a Performance Budgetting tool for .NET. 
It allows you to set performance targets and take action if those targets are not met.

## Getting Started with Budgerigar

    
    var budgetter = new PerformanceBudgetter();
    var response = await budgetter.RunWithBudgetAsync("some-task-name", 1.0M, async (b) => { 
        // do work  
    
    }, (result) => {
        if(result.IsOver) {
            logger.Error(result.GetDetailedOutput());
        }
    });


## Full Example

TODO (soon)