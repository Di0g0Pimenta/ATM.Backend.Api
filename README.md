# ATM.Backend.Api

REST API developed in ASP.NET Core to support an ATM banking system with complete financial operations.

## 📋 Project Overview

Academic project for Object-Oriented Programming course, implementing a complete ATM system with authentication, banking operations, and transaction management.

## ✅ Implementation Checklist

### **MANDATORY REQUIREMENTS (50%)**

#### **1. Core Entities & Relationships**
- [ ] **Bank** entity
- [ ] **Client** entity
- [ ] **Account** entity
- [ ] **Card** entity
- [ ] **Transaction/Movement** entity
- [ ] Relationships implemented:
  - [ ] Multiple Banks exist in system
  - [ ] Clients can have Accounts in multiple Banks
  - [ ] Each Account has multiple Cards
  - [ ] Each Card has a list of Movements/Transactions

#### **2. Essential Banking Operations**

**2.1 Check Balance**
- [ ] Load balance from database
- [ ] Load movements/transactions from database
- [ ] Display balance in UI after login

**2.2 Withdraw Money**
- [ ] Validate numeric and positive input
- [ ] Check sufficient balance
- [ ] Create transaction record in database
- [ ] Update account balance in database
- [ ] Display confirmation message

**2.3 Deposit Money**
- [ ] Validate numeric and positive input
- [ ] Update client balance
- [ ] Register operation in database
- [ ] Display confirmation message

**2.4 Transaction History**
- [ ] Display complete list of client movements
- [ ] Implement filtering options
- [ ] Show transaction details (date, type, amount)

#### **3. Technical Requirements**

**3.1 Object-Oriented Programming (OOP)**
- [ ] Separate classes for each entity:
  - [ ] Account class
  - [ ] Movement/Transaction class
  - [ ] Client class
  - [ ] Bank class
- [ ] **Encapsulation** properly implemented
- [ ] **Inheritance** implemented (minimum 1 case)
  - [ ] Example: Operation → Withdrawal / Deposit / Transfer
- [ ] **Polymorphism** applied to operations

**3.2 Database Access**
- [ ] SQL Server connection configured
- [ ] All entities persisted in database
- [ ] CRUD operations for all entities
- [ ] Transaction management implemented

**3.3 Security & Validation**
- [ ] Prevent negative values
- [ ] Prevent non-numeric characters
- [ ] Appropriate error messages
- [ ] Exception handling throughout application
- [ ] Secure credential validation

---

### **ADDITIONAL FEATURES (Choose to implement)**

#### **4. Advanced Functionalities**
- [ ] **Transfers**
  - [ ] Between different banks
  - [ ] Between accounts in same bank
  - [ ] Transfer history
- [ ] **Bill Payments** simulation
  - [ ] Water bills
  - [ ] Electricity bills
  - [ ] Phone bills
  - [ ] Other services
- [ ] **Improved UI**
  - [ ] Modern interface design
  - [ ] Responsive layout
  - [ ] User-friendly navigation
- [ ] **Dashboard with charts**
  - [ ] Spending analytics
  - [ ] Income vs expenses
  - [ ] Category breakdowns
- [ ] **Daily withdrawal limits**
  - [ ] Configurable limits per account
  - [ ] Limit tracking
  - [ ] Alert system

---

### **EVALUATION CRITERIA (50%)**

#### **1. Creativity & Innovation**
- [ ] Original solutions proposed
- [ ] UI improvements beyond minimum requirements
- [ ] New useful features not specified in requirements
- [ ] Innovative approach to problems

#### **2. Autonomy & Initiative**
- [ ] Features implemented autonomously
- [ ] Additional classes created appropriately
- [ ] Problem-solving without detailed instructions
- [ ] Proactive improvements

#### **3. Relevance & Conceptual Depth**
- [ ] Proper relationships between entities documented
- [ ] Inheritance hierarchy explained
- [ ] Polymorphism usage justified
- [ ] Architectural decisions documented

#### **4. Collaboration & Collective Thinking**
- [ ] Group discussions documented (if applicable)
- [ ] Collaborative problem-solving evidenced
- [ ] Contributions acknowledged

#### **5. Analytical Thinking & Problem Solving**
- [ ] Complex validations implemented
- [ ] Balance checks working correctly
- [ ] Limit validations functioning
- [ ] Database error handling
- [ ] SQL transactions managed properly
- [ ] Efficient operation logic

#### **6. Use of AI Tools (Ethical & Reflective)**
- [ ] AI tools used responsibly
- [ ] AI assistance cited in report
- [ ] Generated code understood and explained
- [ ] Manual adjustments documented
- [ ] No copy-paste approach

#### **7. Self-Learning & Knowledge Expansion**
- [ ] Research beyond class material
- [ ] New libraries/patterns explored
- [ ] Advanced SQL Server features used
- [ ] Application expanded beyond minimum

#### **8. Communication & Explanation**
- [ ] Clear model explanation in report
- [ ] Decisions justified
- [ ] Logical application demonstration
- [ ] Clean code documentation

#### **9. Impact & Contribution**
- [ ] Stable application functionality
- [ ] Significant improvements to base core
- [ ] Added value to ATM ecosystem

#### **10. Comprehensive Report/Artifact**
- [ ] Professional structure
- [ ] Class architecture described
- [ ] Functional flows explained
- [ ] Screenshots included
- [ ] Difficulties and solutions documented

---

## 📦 Deliverables

### **Required Submissions**
1. **Source Code**
   - [ ] Well-structured and organized
   - [ ] Commented appropriately
   - [ ] Follows coding conventions

2. **SQL Script**
   - [ ] Database creation script
   - [ ] Table definitions
   - [ ] Sample data (optional)
   - [ ] Relationships and constraints

3. **Technical Report**
   - [ ] Class explanations
   - [ ] Architecture overview
   - [ ] Decision justifications
   - [ ] Application screenshots
   - [ ] UML diagrams (optional)
   - [ ] Challenges faced and solutions

4. **Final Presentation/Defense**
   - [ ] 15-minute demonstration prepared
   - [ ] Live demo ready
   - [ ] Q&A preparation

---

## 🛠️ Technologies

- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server (or SQLite planned)
- **Documentation**: Swagger (OpenAPI)
- **Version Control**: GitHub
- **Additional**: (add as implemented)

## 🏗️ Architecture

```
ATM.Backend.Api/
├── Controllers/          # API endpoints
├── Services/            # Business logic
├── Repositories/        # Data access layer
├── Models/             # Entity models
├── DTOs/               # Data transfer objects
├── Database/           # DB context & migrations
└── Utilities/          # Helper classes
```

## 📊 Database Schema

### Core Tables Required
- **Banks** - Bank information
- **Clients** - Customer data
- **Accounts** - Account details
- **Cards** - ATM card information
- **Movements** - Transaction history

## 📝 API Documentation

Access Swagger UI at: `https://localhost:5001/swagger` (after running, not Working)

---

## 👨‍💻 Author

**Diogo Pimenta**,
**José Soares**,
**João Borges**,
**Olavo**
- Academic project for Object-Oriented Programming course
- GitHub: [@Di0g0Pimenta](https://github.com/Di0g0Pimenta)

---

## 📄 License

This is an academic project. All rights reserved.

---

## 📌 Notes

- This project is being developed as part of an academic assignment
- Regular commits and proper version control are essential
- Follow OOP principles strictly
- Document all major decisions in the technical report
- Test thoroughly before final submission
