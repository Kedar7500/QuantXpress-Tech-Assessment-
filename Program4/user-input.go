package main

import "fmt"

func main2() {

	var fname string
	fmt.Println("Enter first name : ")
	fmt.Scanln(&fname)

	var lname string
	fmt.Println("Enter last name : ")
	fmt.Scanln(&lname)

	fmt.Print("Full name is " + fname + " " + lname)

}
