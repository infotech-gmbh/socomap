# NOTICE

This project is in an early alpha stage.
It will be improved continously.

# Quickstart

If you just want to run your own socomap server you need an installed [Docker](https://www.docker.com/get-started)

Download this [docker-compose.yml](https://github.com/infotech-gmbh/socomap-samples/blob/master/samples/server/docker-compose.yml) file and run it:

```sh
curl -o docker-compose.yml https://raw.githubusercontent.com/infotech-gmbh/socomap-samples/master/samples/server/docker-compose.yml
docker-compose up
```

The data is stored in a named docker volume called socomap.

# Introduction

Socomap is designed to enable a point to point data transfer.
The receiver have to register an inbox on a socomap server.
The sender dont have to be registered anywhere and can send
messages to each inboxes.
The protocol is designed to have a reliable data transfer, where
the sender is informed when the receiver got the message (confirmed by receiver).

![architecture image](docs/img/peer_to_peer_with_broker.xml)

The transferred messages can completely be specified by the user agents and can contain binary data.

Example clients can be found [here](https://github.com/infotech-gmbh/socomap-samples)

# Features

* Aynchronous binary message transfer between two parties
* Built on standard components (OpenAPI3)
* End to end encryption as first class function (TODO)
* Easy to use libraries (under construction) (TODO)
* Flexible certificate management in user space (TODO)
* No manual maintenance or user management
* Sample client [implementations](https://github.com/infotech-gmbh/socomap-samples)
* Support for popular cloud tools eg. Prometheus, Graylog and DockerSwarm (TODO)

