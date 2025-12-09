#pragma once
#include "day.hpp"
#include <vector>


class Day3 : public Day {
public:
    std::string solvePartA(const std::string& input) override;
    std::string solvePartB(const std::string& input) override;
private:
    std::string solve(const std::string& input, const int& batteries);

    std::vector<std::vector<short>> parseBatteryBanks(const std::string& input);
    std::pair<short, size_t> largestBattery(const std::vector<short>& bank, const size_t& start, const size_t& end);
    
    std::string getLargestJoltage(const std::vector<short>& bank, const int batteryCount);
};