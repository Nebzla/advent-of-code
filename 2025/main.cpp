#include <fstream>
#include <iostream>
#include <memory>
#include <sstream>
#include <stdexcept>
#include <string>
#include <unordered_map>

#include "day.hpp"
#include "day_1.hpp"
#include "day_2.hpp"
#include "day_3.hpp"

std::unordered_map<int, std::unique_ptr<Day>> days;

void registerDays() {
    days[1] = std::make_unique<Day1>();
    days[2] = std::make_unique<Day2>();
    days[3] = std::make_unique<Day3>();
}

std::string getDayInput(const int& dayNum) {
    std::ifstream file("../inputs/day_" + std::to_string(dayNum) + ".txt");
    if(!file) throw std::invalid_argument("Can't find a file of associated day number");
    
    std::stringstream buffer;
    buffer << file.rdbuf();
    std::string contents = buffer.str();

    file.close();

    return contents;
}

void execDay(const int& dayNum) {
    auto it = days.find(dayNum);
    if(it == days.end() || !it->second) throw std::runtime_error("Day is not implemented");

    std::unique_ptr<Day>& day = it->second;
    day->solve(getDayInput(dayNum));
}

const int FALLBACK_DAY_EXEC = 2;

int main(int argc, char** argv) {
    registerDays();

    int dayNum;
    if(argc < 2) {
        dayNum = FALLBACK_DAY_EXEC;
        std::cout << "No day argument specified, falling back to default day." << std::endl;
    } else {
        try { 
            dayNum = std::stoi(argv[1]);
        } catch (const std::invalid_argument&) {
            throw std::invalid_argument("Day argument must be an integer");
        }
    }

    execDay(dayNum);
    return 0;
}