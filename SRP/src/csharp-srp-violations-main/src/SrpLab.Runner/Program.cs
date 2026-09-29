using SrpLab;
using SrpLab.AppointmentDeskRefactoring;
using SrpLab.CheckoutBasketRefactoring;
using SrpLab.CourseEnrollmentRefactoring;
using SrpLab.GradeBookRefactoring;
using SrpLab.KitchenTicketRefactoring;
using SrpLab.LoanDeskRefactoring;
using SrpLab.SubscriptionRefactoring;
using SrpLab.SupportTicketRefactoring;
using SrpLab.WardBoardRefactoring;
using SrpLab.WarehouseRefactoring;

//Console.WriteLine("SrpLab — 10 intentional SRP violations (refactor me)");
//Console.WriteLine("=================================================");

//var ward = new WardBoard();
//ward.AssignBed(1, "p-88", heartRate: 130, spo2: 89);
//Console.WriteLine(ward.BuildHandoffNote(1));
//Console.WriteLine(string.Join(" | ", ward.DrainPagerLog()));

//var basket = new CheckoutBasket();
//basket.AddLine("SKU-1", 40m, 2);
//basket.ApplyCouponText("SAVE10");
//basket.EnableGiftWrap();
//Console.WriteLine($"basket total={basket.GrandTotal()} auth={basket.AuthorizePaymentStub("4242")}");

//var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
//Console.WriteLine(ticket.DraftPublicReply("Nora"));

//var loan = new LoanDesk(60_000m, 640, 4, hasCollateral: false);
//Console.WriteLine(loan.DecisionLetter("Omar"));

//var course = new CourseEnrollmentDesk("SEF-101", capacity: 1, tuition: 3000m);
//Console.WriteLine(course.Register("a@mail.com"));
//Console.WriteLine(course.Register("b@mail.com"));
//Console.WriteLine(course.WelcomePacketMarkdown("b@mail.com", "Bea"));

//var kitchen = new KitchenTicket();
//kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
//Console.WriteLine(kitchen.RenderThermalTicket(42));

//var sub = new SubscriptionBilling("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
//sub.RegisterFailedPayment();
//Console.WriteLine(sub.DunningEmail("Sara", new DateOnly(2026, 9, 20)));

//var pick = new WarehousePickList();
//pick.AddNeed("BOLT", "A", 3, 10, 7);
//pick.AddNeed("NUT", "B", 1, 5, 5);
//Console.WriteLine(pick.PickerScript());

//var grades = new GradeBook();
//grades.Record("s1", 92);
//grades.Record("s1", 88);
//Console.WriteLine(grades.TranscriptPlain("s1", "Ali"));

//var appt = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
//var slot = appt.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
//if (slot is null) throw new InvalidOperationException("no slot");
//appt.TryBook(slot.Value);
//Console.WriteLine(appt.SmsReminder(slot.Value, "0100"));

Console.WriteLine("Done. Now split responsibilities — without breaking behavior.");

Console.WriteLine("=================================================");
Console.WriteLine();

Console.WriteLine("AppointmentDesk After Splitting Responsibilites: ");
Console.WriteLine();


var schedule = new ClinicSchedule(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);

var scheduler = new AppointmentScheduler(schedule);

var from = new DateTimeOffset(
    2026, 9, 24, 9, 15, 0,
    TimeSpan.Zero);

var slot = scheduler.FindNextSlot(from, 8);

if (slot is not null)
{
    var booked = scheduler.TryBook(slot.Value);

    Console.WriteLine($"Slot: {slot}");
    Console.WriteLine($"Booked: {booked}");

    var calendar = new AppointmentCalendarFormatter();
    Console.WriteLine(
        calendar.ToIcs(
            slot.Value,
            schedule.SlotMinutes,
            "Ahmed",
            "Dr. Ali"));

    var sms = new AppointmentSmsFormatter();
    Console.WriteLine(
        sms.CreateReminder(
            slot.Value,
            "01000000000"));
}

Console.WriteLine("=================================================");
Console.WriteLine();


Console.WriteLine("CheckoutBasket After Splitting Responsibilites: ");
Console.WriteLine();

var cart = new CheckoutCart();

cart.AddLine("LAPTOP", 1000m, 1);
cart.AddLine("MOUSE", 50m, 2);

var coupon = new CouponDiscountCalculator();
coupon.ApplyCouponText("SAVE10");

var subTotal = cart.SubTotal();
var discount = coupon.CalculateDiscount(subTotal);

var totalCalculator = new CheckoutTotalCalculator();

var grandTotal = totalCalculator.Calculate(
    subTotal,
    discount,
    true);

Console.WriteLine($"Subtotal: {subTotal:C}");
Console.WriteLine($"Discount: {discount:C}");
Console.WriteLine($"Grand Total: {grandTotal:C}");

var giftCard = new GiftMessageCardFormatter();

Console.WriteLine("\n--- Gift Message ---");
Console.WriteLine(
    giftCard.Create(
        "Ahmed",
        cart.Skus,
        grandTotal));

var payment = new PaymentAuthorizer();

Console.WriteLine("--- Payment ---");
Console.WriteLine(
    payment.Authorize(
        grandTotal,
        "1234",
        cart.LineCount));

Console.WriteLine();
Console.WriteLine("CourseEnrollmentDesk After Splitting Responsibilites: ");
Console.WriteLine();

var course = new CourseDetails("CS101", 1000m);

var enrollment = new CourseEnrollment(2);

Console.WriteLine(
    enrollment.Register("ahmed@example.com"));

Console.WriteLine(
    enrollment.Register("ali@example.com"));

Console.WriteLine(
    enrollment.Register("mohamed@example.com"));

Console.WriteLine(
    $"Waitlist position: " +
    $"{enrollment.WaitlistPosition("mohamed@example.com")}");

enrollment.PromoteFromWaitlist(1);

var welcome = new CourseWelcomePacketFormatter();

Console.WriteLine("\n--- Welcome Packet ---");

Console.WriteLine(
    welcome.Create(
        course.CourseCode,
        "mohamed@example.com",
        "Mohamed",
        enrollment));

var invoice = new TuitionInvoiceFormatter();

Console.WriteLine("--- Invoice ---");

Console.WriteLine(
    invoice.Create(
        course.CourseCode,
        "mohamed@example.com",
        course.Tuition,
        enrollment));

Console.WriteLine();
Console.WriteLine("GradeBook After Splitting Responsibilites: ");
Console.WriteLine();

var records = new StudentGradeRecords();

records.Record("S001", 90);
records.Record("S001", 85);
records.Record("S002", 70);
records.Record("S002", 75);

var gradePolicy = new GradePolicy();
var honorRollPolicy = new HonorRollPolicy();

var average = records.Average("S001");
var letter = gradePolicy.Letter(average);
var honor = honorRollPolicy.MeetsHonorRoll(average, letter);

Console.WriteLine($"Average: {average}");
Console.WriteLine($"Letter: {letter}");
Console.WriteLine($"Honor: {honor}");

var transcript = new TranscriptFormatter();

Console.WriteLine("\n--- Transcript ---");
Console.WriteLine(
    transcript.Create(
        "S001",
        "Ahmed",
        average,
        letter,
        honor));

var exporter = new GradeCsvExporter();

Console.WriteLine("--- CSV ---");
Console.WriteLine(
    exporter.Export(
        records,
        gradePolicy,
        honorRollPolicy));

Console.WriteLine();
Console.WriteLine("KitchenTicket After Splitting Responsibilites: ");
Console.WriteLine();

var order = new KitchenOrder();

order.AddItem(
    "Chicken Sandwich",
    new[] { "Bread", "Chicken", "Butter" },
    15
);

order.AddItem(
    "Shrimp Pasta",
    new[] { "Pasta", "Shrimp", "Cheese" },
    20
);

var allergenDetector = new AllergenDetector();

var readinessCalculator =
    new KitchenReadinessCalculator(allergenDetector);

var ticketFormatter =
    new ThermalTicketFormatter(
        allergenDetector,
        readinessCalculator);

Console.WriteLine("Allergens:");
Console.WriteLine(string.Join(", ", allergenDetector.Detect(order)));

Console.WriteLine();

Console.WriteLine("Estimated Ready Time:");
Console.WriteLine(
    readinessCalculator.EstimatedReadyMinutes(order, 2)
);

Console.WriteLine();

Console.WriteLine("Thermal Ticket:");
Console.WriteLine(
    ticketFormatter.Render(order, 1234)
);

Console.WriteLine("Expo Lane:");
Console.WriteLine(
    ticketFormatter.ExpoLaneHint(order)
);

Console.WriteLine();
Console.WriteLine("LoanDesk After Splitting Responsibilites: ");
Console.WriteLine();

var application = new LoanApplication(
    requestedAmount: 75_000m,
    creditScore: 650,
    employmentMonths: 18,
    hasCollateral: true
);

var riskEvaluator = new LoanRiskEvaluator();

var documentRequirements =
    new LoanDocumentRequirements(riskEvaluator);

var decisionFormatter =
    new LoanDecisionFormatter(
        riskEvaluator,
        documentRequirements);

Console.WriteLine($"Risk Score: {riskEvaluator.RiskScore(application):0.00}");

Console.WriteLine(
    $"Eligible: {riskEvaluator.IsEligible(application)}"
);

Console.WriteLine("\nRequired Documents:");

foreach (var document in documentRequirements.GetRequiredDocuments(application))
{
    Console.WriteLine($"- {document}");
}

Console.WriteLine("\nDecision Letter:");
Console.WriteLine(
    decisionFormatter.DecisionLetter(application, "Ahmed")
);

Console.WriteLine("Underwriter CSV:");
Console.WriteLine(
    decisionFormatter.UnderwriterCsvRow(application, "APP-1001")
);

Console.WriteLine();
Console.WriteLine("SubscriptionBilling After Splitting Responsibilites: ");
Console.WriteLine();

var subscription = new Subscription(
    customerId: "CUS-1001",
    monthlyPrice: 100m,
    periodStart: new DateOnly(2026, 9, 1),
    periodEnd: new DateOnly(2026, 10, 1)
);

var prorationCalculator = new SubscriptionProrationCalculator();
var invoiceGenerator = new InvoiceNumberGenerator();
var paymentFailures = new PaymentFailureTracker();

paymentFailures.RegisterFailedPayment();
paymentFailures.RegisterFailedPayment();

Console.WriteLine(
    $"Prorated Amount: " +
    $"{prorationCalculator.Prorate(subscription, new DateOnly(2026, 9, 15)):0.00}"
);

var formatter = new BillingOutputFormatter(
    prorationCalculator,
    invoiceGenerator
);

Console.WriteLine("\nDunning Email:");
Console.WriteLine(
    formatter.DunningEmail(
        subscription,
        paymentFailures,
        "Ahmed",
        new DateOnly(2026, 9, 25))
);

Console.WriteLine("Ledger Journal Line:");
Console.WriteLine(
    formatter.LedgerJournalLine(
        subscription,
        new DateOnly(2026, 9, 15))
);

Console.WriteLine();
Console.WriteLine("SupportTicket After Splitting Responsibilites: ");
Console.WriteLine();

var ticket = new SupportTicketInfo(
    id: "T-1001",
    subject: "Cannot login to the system",
    body: "The customer is blocked from accessing the application.",
    openedAt: DateTimeOffset.UtcNow
);

ticket.AppendCustomerMessage(
    "Please fix this ASAP."
);

var priorityDecider = new TicketPriorityDecider();
var slaPolicy = new TicketSlaPolicy();
var communicationFormatter =
    new SupportCommunicationFormatter(slaPolicy);

var priority = priorityDecider.CalculatePriority(ticket);

Console.WriteLine($"Ticket: {ticket.Id}");
Console.WriteLine($"Priority: {priority}");

var deadline = slaPolicy.SlaDeadline(ticket, priority);

Console.WriteLine($"SLA Deadline: {deadline:u}");

Console.WriteLine(
    $"SLA Breached: " +
    $"{slaPolicy.IsBreached(ticket, priority, DateTimeOffset.UtcNow)}"
);

Console.WriteLine("\nPublic Reply:");
Console.WriteLine(
    communicationFormatter.DraftPublicReply(
        ticket,
        priority,
        "Ahmed")
);

Console.WriteLine("Internal Escalation:");
Console.WriteLine(
    communicationFormatter.InternalEscalationBlurb(
        ticket,
        priority)
);

Console.WriteLine();
Console.WriteLine("WardBoard After Splitting Responsibilites: ");
Console.WriteLine();

var board = new WardBedBoard();

var acuityScorer = new PatientAcuityScorer();
var pagerLog = new PagerAlertLog();

board.AssignBed(
    bed: 12,
    patientId: "p-1001",
    heartRate: 130,
    spo2: 88,
    acuityScorer,
    pagerLog
);

board.AssignBed(
    bed: 8,
    patientId: "p-1002",
    heartRate: 85,
    spo2: 97,
    acuityScorer,
    pagerLog
);

var handoffFormatter = new WardHandoffFormatter();
var censusExporter = new WardCensusCsvExporter();

Console.WriteLine("Handoff:");
Console.WriteLine(handoffFormatter.Build(board, 12));

Console.WriteLine("\nEmpty Bed:");
Console.WriteLine(handoffFormatter.Build(board, 20));

Console.WriteLine("\nPager Alerts:");

foreach (var alert in pagerLog.Drain())
{
    Console.WriteLine(alert);
}

Console.WriteLine("\nCensus CSV:");
Console.WriteLine(censusExporter.Export(board));

Console.WriteLine();
Console.WriteLine("WarehousePickList After Splitting Responsibilites: ");
Console.WriteLine();

var pickList = new WarehousePickList();

pickList.AddNeed(
    sku: "SKU-100",
    aisle: "A",
    bin: 10,
    qtyNeeded: 5,
    qtyOnHand: 8
);

pickList.AddNeed(
    sku: "SKU-200",
    aisle: "B",
    bin: 3,
    qtyNeeded: 10,
    qtyOnHand: 6
);

pickList.AddNeed(
    sku: "SKU-300",
    aisle: "A",
    bin: 5,
    qtyNeeded: 2,
    qtyOnHand: 2
);

var allocator = new InventoryAllocator();
var walkingPlanner = new WarehouseWalkingPlanner();

var outputFormatter = new WarehouseOutputFormatter(
    walkingPlanner,
    allocator
);

Console.WriteLine("Allocated Items:");

foreach (var allocation in allocator.Allocate(pickList))
{
    Console.WriteLine(
        $"{allocation.Sku}: {allocation.Allocated}");
}

Console.WriteLine("\nWalking Order:");
foreach (var step in walkingPlanner.CreateWalkingOrder(pickList))
{
    Console.WriteLine(
        $"{step.Aisle}-{step.Bin}: {step.Sku} × {step.Qty}");
}

Console.WriteLine("\nPicker Script:");
Console.WriteLine(
    outputFormatter.PickerScript(pickList));

Console.WriteLine("\nWMS XML:");
Console.WriteLine(
    outputFormatter.WmsXmlBatch(pickList, "BATCH-001"));