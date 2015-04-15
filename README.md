# Deployment Cockpit

Deployment Cockpit is a deployment automation tool. It is built around the concept of multiple small reusable scripts which, combined with project definitions, are executed in specified order.


## System Overview

Deployment Cockpit consists of three main components: Server, Job Runner and Targets.

**Server** is a web application where all scripts and project definitions are stored and managed. It is the central place of the system from which deployments are initiated and monitored.

**Job Runner** is a windows service responsible for execution of deployment jobs initiated by the Server. It usually (but not necessary) runs on the same machine as the Server.

**Target** is a windows service running on target machines. Its purpose is to execute scripts received from Job Runner and return back execution results.


## Logical Structure

There are following entities that can be defined in Deployment Cockpit:

**Projects** are definitions of the applications that are being deployed using Deployment Cockpit. Project consists of Target Groups, Environments, Project Targets, Deployment Plans, Variables, Deployment Jobs.

**Target Groups** are groups of target machines to which specific part of the application is deployed. (e.g. administration web site can be one target group, web shop can be second target group, and order processor service can be third target group)

**Environments** are pretty much self explanatory (e.g. Development, QA, Production).

**Project Targets** are target machines defined for every combination of Target Group and Environment.

**Deployment Plans** are lists of steps which are supposed to be executed for deployment to be complete.

**Deployment Plan Step** is one of the steps in Deployment Plan which specifies the script that should be executed in that step. It also specifies if the script should be executed for specific or all target groups and if execution is supposed to happen remotely on target machine (e.g. start/stop windows service, set IIS application offline) or locally on deployment server by using information about target machine (e.g. pushing deployment package to shared directory on target machine). Execution can also be done from deployment server without specific target group (e.g. update database by running SQL scripts against DB server)

**Deployment Jobs** are used to initiate deployment by specifying Deployment Plan which should be used, version of the application to be deployed, and target Environment.

**Targets** are machines on which applications/projects managed by Deployment Cockpit are being deployed.

**Credentials** are usernames and passwords stored in the system which can be assigned to multiple targets.

**Scripts** are small, focused scripts intended to be reusable across the projects, but can also be project-specific. They can be written in Batch or PowerShell and can have multiple parameters defined. Values for defined script parameters are supplied by variables during deployment job execution.

**Variables** are used to provide values for script parameters during deployment execution. Variables can be defined on eight levels:
- global
- project
- target group
- environment
- target group / environment combination
- project target (one of the machines belonging to target group / environment combination)
- deployment plan
- deployment plan step
