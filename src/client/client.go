package main

import (
	"log"
	"net"
	"time"
)

func main() {
	log.Println("Starting MoraES 🚀")
	ln, err := net.Dial("tcp", "localhost:2626")
	if err != nil {
		log.Fatal(err)
	}
	for {
		log.Println("Writing Ciccio...")
		size, err := ln.Write([]byte("Ciccio\n"))
		if err != nil {
			log.Fatal(err)
		}

		log.Printf("Wrote Ciccio, for a total of %d bytes", size)
		time.Sleep(time.Millisecond * 5)
	}
}
