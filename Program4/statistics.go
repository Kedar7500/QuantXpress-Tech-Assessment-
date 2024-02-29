package main

import (
	"fmt"
	"sort"
)

// 1. Calculate the maen of series
func mean(series []int) float64 {

	sum := 0

	for _, num := range series {
		sum += num
	}
	return float64(sum) / float64(len(series))
}

// 2. meadian of the series
func meadian(series []int) float64 {

	sortSeries := make([]int, len(series))
	copy(sortSeries, series)
	sort.Ints(sortSeries)
	mid := len(sortSeries) / 2

	if len(sortSeries)%2 == 0 {
		return float64(sortSeries[mid-1]+sortSeries[mid]) / 2.0
	}
	return float64(sortSeries[mid])
}

// 3. function to calculate the mode
func mode(series []int) []int {

	freqMap := make(map[int]int)
	maxFreq := 0

	for _, num := range series {
		freqMap[num]++
		if freqMap[num] > maxFreq {
			maxFreq = freqMap[num]
		}
	}

	modes := []int{}
	for num, freq := range freqMap {
		if freq == maxFreq {
			modes = append(modes, num)
		}
	}
	return modes
}

// 4. function to calculate standard deviation
func standardDeviation(series []int) float64 {
	meanVal := mean(series)

	sumSquaredDiff := 0.0

	for _, num := range series {
		diff := float64(num) - meanVal
		sumSquaredDiff += diff * diff

	}
	return (sumSquaredDiff / float64(len(series)))

}

// 5. function to find primenumber
func primenumber(series []int) []int {

	primes := []int{}
	for _, num := range series {
		if isPrime(num) {
			primes = append(primes, num)
		}
	}
	return primes

}

func isPrime(num int) bool {

	if num <= 1 {
		return false
	}

	for i := 2; i*i <= num; i++ {
		if num%i == 0 {
			return false
		}
	}
	return true
}

// 6. Function for binary search
func binarySerach(series []int, target int) bool {

	low, high := 0, len(series)-1
	for low <= high {
		mid := (low + high) / 2
		if series[mid] == target {
			return true
		} else if series[mid] < target {
			low = mid + 1
		} else {
			high = mid - 1
		}
	}
	return false

}
func main() {

	var series []int

	var num, n int

	fmt.Print("Enter the number of elemets in series : ")
	fmt.Scanln(&n)

	fmt.Print("Enter the elements of the series : ")
	for i := 0; i < n; i++ {
		fmt.Scanln(&num)

		series = append(series, num)
	}

	// to calculate mean
	fmt.Printf("Mean of the series : ", mean(series))

	// to calculate median
	fmt.Printf("Median of the series : ", meadian(series))

	// to calculate mode
	fmt.Printf("Mode of the series : ", mode(series))

	// to calculate standard deviation
	fmt.Printf("Standard deviation of series : ", standardDeviation(series))

	// to calculate series in ascending order
	sort.Ints(series)
	fmt.Println("sorted series :", series)

	// to calculate max and min of series
	fmt.Println("Maximum of series : ", series[len(series)-1])
	fmt.Println("Min of the series : ", series[0])

	// calculate prime numbers of series
	primes := primenumber(series)
	fmt.Println("Prime numbers of series ", primes)

	// for binary search

	var target int
	fmt.Print("Enter the number have to search in series : ")
	fmt.Scanln(&target)

	if binarySerach(series, target) {
		fmt.Println(target, "Found in the series ")
	} else {

		fmt.Println("Target not found in series ")
	}

}
