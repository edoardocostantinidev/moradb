package main

import (
	"bufio"
	"log"
	"net"
)

func main() {
	log.Println("Starting MoraES 🚀")
	ln, err := net.Listen("tcp", "localhost:2626")
	if err != nil {
		log.Fatal(err)
	}
	defer ln.Close()
	conn, err := ln.Accept()
	if err != nil {
		log.Fatal(err)
	}
	for {
		log.Println("Received from: (remote) ", conn.RemoteAddr())
		log.Println("Received from: (local) ", conn.LocalAddr())
		msg, err := bufio.NewReader(conn).ReadString('\n')
		if err != nil {
			log.Fatal(err)
		}
		log.Println("Received from: (msg) ", string(msg))
	}
}
