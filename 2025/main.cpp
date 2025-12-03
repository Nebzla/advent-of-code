#include <fstream>
#include <memory>
#include <sstream>
#include <stdexcept>
#include <string>
#include <unordered_map>

#include "day.hpp"
#include "day_1.hpp"
#include "benchmark.hpp"

std::unordered_map<int, std::unique_ptr<Day>> days;

void registerDays() {
    days[1] = std::make_unique<Day1>();
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
    day->outputSolutions(getDayInput(dayNum));
}

int main(int argc, char** argv) {
    // if(argc < 2) throw std::invalid_argument("Missing second argument, must specify a day to test");

    registerDays();

    // int dayNum;
    // try { 
    //     dayNum = std::stoi(argv[1]);
    // } catch (const std::invalid_argument&) {
    //     throw std::invalid_argument("Day argument must be an integer");
    // }

    // execDay(dayNum);

    execDay(1);

    return 0;
}