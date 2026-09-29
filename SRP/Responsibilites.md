## Class 1 - AppointmentDesk

Responsibilities identified: 

1. Manage Business Hours

- Stores open and close hours
- IsWithinBusinessHours() checks whether an appointment falls within working hours
- Reason to change: working hours policy could be changed

2. Manage Appointment Slots

- Storing slot minutes
- Slign a time to the appointment withing specific boundaries
- Search for the next available slot
- Reason to change: The appointment slot rules could change, such as slot duration or how the next slot is calculated

3. Booking Appointments

- Checks availability and adds a booked slot
- Reason to change: The way appointments are booked, cancelled, or checked for availability could change

4. Generate calendar data

- Creates a calendar format representation of an appointment
- Reason to change: Calendar format may need to change

5. Generate SMS reminder messages

- reminding clients about appointments
- Reason to change: SMS message itself, or communication channels could change

## Why having them together is a problem?

- AppointmentDesk has multiple unrelated reasons to change which violates the srp,
 such as the clinic could change its opening hours without changing appointment booking

- The class is responsible for scheduling business hours, booking state,
  calendar formatting, and messaging, which are separate concerns

## Class 2 - CheckoutBasket

Responsibilities identified: 

1. Manage basket items

- Stores Basket Items & add products with validation
- Reason to change: Basket items management rules could change, such as how items are added, removed

2. Applying coupon and discount

- Storing and parsing the coupon string
- Reason to change: coupon formats and discount rules might be changed

3. Calculating basket items

- calculates subtotal and the final amount
- Reason to change: The application's calculation rules could change

4. Manage packaging price

- Check if git wrapping is enabled or not and applying additional fees
- Reason to change: Packaging pricing or policy could be changed

5. Generate customer gifting messages

- Reason to change: the message could change

6. Authorize payments

- creating a payment authorization
- Reason to change: Payment gateway or authorization rules could change

## Why having them together is a problem?

- CheckoutBasket handles alot of responsibilities that have different reasons to change
  which violates the srp such as Basket management, Pricing calculations, etc..

- it make the class less cohesive, It combines shoppingstate, pricing, marketing, packaging, presentation, and payment into one class

## Class 3 - CourseEnrollmentDesk

Responsibilities identified: 

1. Manage student enrollment & seat allocation

- Reason to change: If the rules for registering students or assigning available seats change

2. Manage the waitlist

- Reason to change: Waitlist rules, ordering, or how positions are calculated could change

3. Promote students from the waitlist

- Reason to change: The rules for moving students from the waitlist into available seats could change

4. Generate the welcome packet

- Reason to change: The content, wording, or marketing requirements can change

5. Generate tuition invoice

Reason to change: Tuition formatting, or finance requirements can change

## Why having them together is a problem?

- CourseEnrollmentDesk has different reasons to change which violates the srp

- the class is responsible for enrollment, waitlists, promotion, marketing content, and financial formatting

## Class 4 - GradeBook

Responsibilities identified: 

1. Store and record student scores

- Reason to change: Rules for storing scores or validating score input could change

2. Calculate student averages

- Reason to change: The calculation rules for averages could change

3. Apply academic grading

- Reason to change: grading criteria can change

4. Generate a plain text transcript

- Reason to change: The transcript format or displayed information can change

5. Export grades to CSV

- Reason to change: CSV columns, formatting, or export requirements can change

## Why having them together is a problem?

- GradeBook is handling scores, calculations, academic policies, document generation, and CSV exporting

- These responsibilities have different reasons to change which violates the srp

## Class 5 - KitchenTicket

Responsibilities identified: 

1. Storing Items

- Storing the collection of ticket items, ingredients, and preparation minutes

- Reason to change: might change the policy of how the items are added

2. Allergen detection

- scanning ingredient strings to flag potential food allergens

- Reason to change: could add or remove allergen or change the policy of how allegerns are detected

3. Kitchen Operations & Calculations

- Calculating preparation timelines, factoring in open station counts, parallel processing capacity, and allergy protocol delays

- Reason to change: could change how preparation timelines are calculated, or policies of allergy delays

4. Kitchen Workflow

- Determining kitchen workflow routing hints based on execution time

- Reason to change: could change the wording or execution time policies

## Why having them together is a problem?

- the class handles alot of responsibilities that have different reasons to change
  which violates the srp

- Tightly coupling: changes to the allergen dictionary or kitchen operational workflows are bound directly to hardware layout templates and string formatting logic

## Class 6 - LoanDesk

Responsibilities identified: 

1. Calculate loan risk

- Reason to change: The risk model and its calculation rules can change

2. Determine loan eligibility

- Reason to change: The criteria for approving or rejecting applicants can change

3. Determine required documents

- Reason to change: Regulatory or compliance requirements can change

4. Generate the decision letter

- Reason to change: Legal wording or communication templates can change

5. Generate the underwriter CSV row

- Reason to change: Analytics export fields and CSV format can change


## Why having them together is a problem?

- LoanDesk is responsible for risk calculation, eligibility decisions, compliance documents, communication, and data export

- These responsibilities have different reasons to change which violates srp

## Class 7 - SubscriptionBilling

Responsibilities identified: 

1. Calculate subscription billing

- Reason to change: Finance calendar and proration rules can change

2. Generate invoice numbers

- Reason to change: The invoice numbering scheme, sequence, or format can change

3. Track failed payments

- Reason to change: Payment-failure tracking or retry rules can change independently of billing calculations.

4. Generate dunning emails

- Reason to change: Collections wording, severity levels, or legal boilerplate can change

5. Generate accounting export data

- Reason to change: The accounting system's export format or required fields can change

## Why having them together is a problem?

- SubscriptionBilling combines billing calculations, invoice numbering, payment failure tracking, customer communication, and accounting export, each area has a different reason to change which violates srp

## Class 8 - SupportTicket

Responsibilities identified:

1. Manage the support ticket data

- Reason to change: The way ticket messages or ticket information are stored and updated could change

2. Determine ticket priority

- Reason to change: The priority rules can change

3. Calculate and monitor SLA

- Reason to change: SLA policies and response time requirements can change

4. Generate support communication text

- Reason to change: Customer templates, tone, or internal escalation formats can change

## Why having them together is a problem?

- SupportTicket combines ticket management, priority detection, SLA calculation, and communication generation,
  These responsibilities have different reasons to change which violates srp

## Class 9 - WardBoard

Responsibilities identified:

1. Manage bed and patient assignments

- Reason to change: Rules for assigning patients to beds or validating bed information could change

2. Calculate acuity

- Reason to change: The scoring rules based on heart rate can change

3. Manage notifications

- Reason to change: Alert thresholds, notification format, or pager behavior can change

4. Generate handoff notes

- The presentation or  format of handoff information can change

5. Export census data to CSV

- Reason to change: The CSV structure or export requirements can change

## Why having them together is a problem?

- WardBoard combines bed management, clinical scoring, alert handling, handoff presentation, and data export,
These responsibilities have different reasons to change which violates srp

## Class 10 - WarehousePickList

Responsibilities identified:

1. Manage warehouse picking requirements

- Reason to change: The way picking requirements are stored or validated could change

2. Allocate available inventory

- Reason to change: Allocation policies can change

3. Determine the walking/picking order

- Reason to change: The warehouse layout or path optimization strategy can change

4. Generate picker instructions

- Reason to change: The wording and UX of instructions for handheld devices can change

5. Generate WMS XML data

- Reason to change: The WMS integration contract or XML structure can change

## Why having them together is a problem?

- WarehousePickList combines pick-list management, inventory allocation, route planning, human instructions, and WMS integration,
 These responsibilities have different reasons to change which violates srp