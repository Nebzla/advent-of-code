#include "day_3.hpp"
#include "string_utils.hpp"
#include <cstddef>
#include <string>
#include <utility>


vector<vector<short>> Day3::parseBatteryBanks(const string& input) {
    vector<string> lines = splitStringByLines(input);
    vector<vector<short>> banks;


    for(const string line : lines) {
        vector<short> bank;

        for (const char c : line) {
            bank.push_back(c - '0');
        }

        banks.push_back(bank);
    }

    return banks;
}

std::pair<short, size_t> Day3::largestBattery(const vector<short>& bank, const size_t& start, const size_t& end) {
    std::pair<short, size_t> largest = std::make_pair(0, -1);

    for(size_t i = start; i < end; i++) {
        short capacity = bank[i];
        
        if(capacity > largest.first) {
            largest.first = capacity;
            largest.second = i;
        }
    }

    return largest;
}


string Day3::getLargestJoltage(const vector<short>& bank, const int batteryCount) {
    int lastBatteryIndex = -1;
    string joltage = "";

    for(int b = 0; b < batteryCount; b++) {
        int batteriesLeft = batteryCount - 1 - b;

        auto largest = largestBattery(bank, lastBatteryIndex + 1, bank.size() - batteriesLeft);
        
        joltage += largest.first + '0';
        lastBatteryIndex = largest.second;
    }

    return joltage;
}


string Day3::solve(const string& input, const int& batteries) {
    const auto banks = parseBatteryBanks(input);
    long totalJoltage = 0;

    for(const auto bank : banks) {
        totalJoltage += std::stol(getLargestJoltage(bank, batteries));
    }

    return std::to_string(totalJoltage);
}




// Largest joltage can be found by finding the largest number in the entire string, and finding the following largest number past that
string Day3::solvePartA(const string& input) {
    return solve(input, 2);
}

string Day3::solvePartB(const string& input) {
    return solve(input, 12);
}