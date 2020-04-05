ALTER PROCEDURE promoteStudents @Studies NVARCHAR(60), @Semester INT
    AS
    DECLARE
        @newIdEnrollment INT;
    BEGIN
        Set XACT_ABORT on;
        BEGIN TRAN
            DECLARE @idStudies INT= (SELECT IdStudy FROM Studies where name = @Studies);
            if @idStudies is null
                BEGIN
                    RAISERROR (15600,-1,-1, 'studia nie istniejaa');
                    return;
                end
            DECLARE @idEnrollment INT =(select IdEnrollment
                                        from Enrollment
                                        where Semester = @Semester
                                          and IdStudy = @idStudies
                                          and StartDate = (select max(StartDate)
                                                           from Enrollment
                                                           where Semester = @Semester
                                                             and IdStudy = @idStudies));
            if @idEnrollment is null
                Begin
                    RAISERROR (15600,-1,-1,'studia nie istnieja');
                    return;
                end

            set @newIdEnrollment = (select IdEnrollment
                                    from Enrollment
                                    where Semester = @Semester + 1
                                      and IdStudy = @idStudies);
            if @newIdEnrollment is null
                BEGIN
                    set @newIdEnrollment = ((select max(IdEnrollment) from Enrollment) + 1);
                    INSERT INTO Enrollment(IdEnrollment, Semester, IdStudy, StartDate)
                    values (@newIdEnrollment, @Semester + 1, @idStudies,
                            (SELECT CAST(GETDATE() AS DATE)));
                end

            UPDATE Student set IdEnrollment=@newIdEnrollment where IdEnrollment = @idEnrollment;
        COMMIT

        select * from Enrollment where IdEnrollment = @newIdEnrollment;
        return;
    end;
