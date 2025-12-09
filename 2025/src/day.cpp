#include "day.hpp"
#include <iostream>
#include <ostream>

using std::cout, std::endl;


void printResults(const string& identifier, const ResultStr result) {
    cout << identifier << " Solution: " << result.returnValue << "\n\n";
    cout << "Avg execution time: " << result.average / 1000 << "μs\n";
    cout << "Quickest iteration: " << result.quickest / 1000 << "μs\n";
    cout << "Slowest iteration: " << result.slowest / 1000 << "μs\n";
}


void Day::solve(const string &input) {
    cout << "Running and benchmarking solutions..." << endl << "-----------------------------------" << endl;

    ResultStr partAResult = benchmark([this, &input] { 
        return this-> solvePartA(input); 
    }, 4);

    ResultStr partBResult = benchmark([this, &input] { 
        return this-> solvePartB(input); 
    }, 4);
    
    printResults("Part A", partAResult);

    cout << "-----------------------------------" << endl;

    printResults("Part B", partBResult);

    cout << "-----------------------------------" << "\n\n";
}