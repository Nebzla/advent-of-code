#include "string_utils.hpp"
#include <sstream>
#include <string>


std::vector<std::string> splitStringByLines(const std::string& text) {
    std::vector<std::string> lines;

    std::stringstream ss(text);
    std::string line;

    while(std::getline(ss, line)) {
        if(!line.empty()) lines.push_back(line);
    }

    return lines;
}

std::vector<std::string> splitStringByDelimiter(const std::string& text, const char& delimiter) {
    std::vector<std::string> substrings;

    std::stringstream ss(text);
    std::string substr;
    
    while(getline(ss, substr, delimiter)) {
        substrings.push_back(substr);
    }

    return substrings;
}