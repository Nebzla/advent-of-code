#include "day_3.hpp"
#include "string_utils.hpp"
#include <cstddef>
#include <string>
#include <utility>
#include <vector>


std::vector<std::vector<short>> Day3::parseBatteryBanks(const std::string& input) {
    std::vector<std::string> lines = splitStringByLines(input);
    std::vector<std::vector<short>> banks;


    for(const std::string line : lines) {
        std::vector<short> bank;

        for (const char c : line) {
            bank.push_back(c - '0');
        }

        banks.push_back(bank);
    }

    return banks;
}

std::pair<short, size_t> Day3::largestBattery(const std::vector<short>& bank, const size_t& start, const size_t& end) {
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


std::string Day3::getLargestJoltage(const std::vector<short>& bank, const int batteryCount) {
    int lastBatteryIndex = -1;
    std::string joltage = "";

    for(int b = 0; b < batteryCount; b++) {
        int batteriesLeft = batteryCount - 1 - b;

        auto largest = largestBattery(bank, lastBatteryIndex + 1, bank.size() - batteriesLeft);
        
        joltage += largest.first + '0';
        lastBatteryIndex = largest.second;
    }

    return joltage;
}


std::string Day3::solve(const std::string& input, const int& batteries) {
    const auto banks = parseBatteryBanks(input);
    long totalJoltage = 0;

    for(const auto bank : banks) {
        totalJoltage += std::stol(getLargestJoltage(bank, batteries));
    }

    return std::to_string(totalJoltage);
}




// Largest joltage can be found by finding the largest number in the entire std::string, and finding the following largest number past that
std::string Day3::solvePartA(const std::string& input) {
    return solve(input, 2);
}

std::string Day3::solvePartB(const std::string& input) {
    return solve(input, 12);
}